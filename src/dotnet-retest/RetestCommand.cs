using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Devlooped.Web;
using Mono.Options;
using NuGet.Packaging;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Rendering;
using static Spectre.Console.AnsiConsole;

namespace Devlooped;

public partial class RetestCommand : AsyncCommand<RetestCommand.RetestSettings>
{
    record TestResult(string FullName, bool Failed);

    public override async Task<int> ExecuteAsync(CommandContext context, RetestSettings settings)
    {
        var args = context.Remaining.Raw.ToList();
        // A typical mistake would be to pass dotnet test args directly without the -- separator
        // so account for this automatically so users fall in the pit of success
        if (args.Count == 0)
        {
            foreach (var key in context.Remaining.Parsed)
            {
                foreach (var value in key)
                {
                    // Revert multiple --key [value] into multiple --key --value
                    args.Add(key.Key);
                    if (value != null)
                        args.Add(value);
                }
            }
        }

        string? path = null;
        var hastrx = false;

        new OptionSet
        {
            { "l|logger=", v => hastrx = v.StartsWith("trx") },
            { "results-directory=", v => path = v },
        }.Parse(args);

        var trx = new TrxCommand.TrxSettings
        {
            Path = path,
            Output = settings.Output,
            Skipped = settings.Skipped,
            GitHubComment = settings.GitHubComment,
            GitHubSummary = settings.GitHubSummary,
        };

        if (trx.Path == null)
        {
            trx.Path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
            args.Insert(0, "--results-directory");
            args.Insert(1, trx.Path);
        }

        // Ensure we add the trx logger. Note that there can be other loggers too
        if (!hastrx)
        {
            args.Insert(0, "--logger");
            args.Insert(1, "trx");
        }

        Debug.Assert(DotnetMuxer.Path != null);

        var failed = new HashSet<string>();
        var attempts = 0;
        BufferedCommandResult? runFailure = null;

        var ci = Environment.GetEnvironmentVariable("CI") == "true";

        ProgressColumn[] columns = ci ?
            [new TaskDescriptionColumn { Alignment = Justify.Left }] :
            [new SpinnerColumn(), new ElapsedTimeColumn(), new MultilineTaskDescriptionColumn()];

        // 11 = spinner + elapsed time + padding
        var maxwith = AnsiConsole.Console.Profile.Width - 11;

        var exitCode = await Progress()
            .Columns(columns)
            .StartAsync(async ctx =>
        {
            while (true)
            {
                attempts++;
                var task = ctx.AddTask($"Running tests, attempt #{attempts}");
                try
                {
                    task.StartTask();

                    // Ensure we don't build on retries after initial attempt, 
                    // just in case --no-build was't passed in the original command.
                    if (attempts > 1 && !args.Contains("--no-build"))
                        args.Insert(0, "--no-build");

                    var prefix = failed.Count > 0 ?
                        $"Running {failed.Count} tests, attempt [yellow]#{attempts}[/]" :
                        $"Running tests, attempt #{attempts}";

                    task.Description = prefix;

                    var exit = await RunTestsAsync(DotnetMuxer.Path.FullName, new List<string>(args), failed, new Progress<string>(line =>
                    {
                        if (ci)
                        {
                            WriteLine(line);
                        }
                        else if (line.Trim() is { Length: > 0 } description)
                        {
                            task.Description = prefix + $": [grey]{description.Substring(0, Math.Min(maxwith - prefix.Length, description.Length))}...[/]";
                        }
                    }));

                    if (exit.ExitCode == 0)
                    {
                        task.Description = prefix + " :check_mark_button:";
                        return 0;
                    }

                    task.Description = prefix + " :cross_mark:";

                    if (!HasTestExpr().IsMatch(exit.StandardOutput) &&
                        !HasTestSummaryExpr().IsMatch(exit.StandardOutput))
                    {
                        runFailure = exit;
                        return exit.ExitCode;
                    }

                    if (attempts >= settings.Attempts)
                        return exit.ExitCode;

                    var outcomes = GetTestResults(trx.Path);
                    // On first attempt, we just batch add all failed tests
                    if (attempts == 1)
                    {
                        failed.AddRange(outcomes.Where(x => x.Value == true).Select(x => x.Key));
                    }
                    else
                    {
                        // Remove from failed the tests that are no longer failed in this attempt
                        failed.RemoveWhere(x => outcomes.TryGetValue(x, out var isFailed) && !isFailed);
                    }
                }
                finally
                {
                    task.StopTask();
                }
            }
        });

        if (runFailure != null)
        {
            MarkupLine($"[red]Error:[/] Failed to run tests.");
            WriteLine(runFailure.StandardOutput);
        }

        if (Directory.Exists(trx.Path))
        {
            new TrxCommand().Execute(context, new TrxCommand.TrxSettings
            {
                GitHubComment = settings.GitHubComment,
                GitHubSummary = settings.GitHubSummary,
                Output = settings.Output,
                Path = trx.Path,
                Skipped = settings.Skipped,
                Recursive = false,
            });
        }

        return exitCode;
    }

    Dictionary<string, bool> GetTestResults(string path)
    {
        var outcomes = new Dictionary<string, bool>();
        if (!Directory.Exists(path))
            return outcomes;

        var ids = new HashSet<string>();

        // Process from newest files to oldest so that newest result we find (by test id) is the one we keep
        // NOTE: we always emit results to a given directory, so we don't need to search for trx files in subdirectories
        foreach (var trx in Directory.EnumerateFiles(path, "*.trx", SearchOption.TopDirectoryOnly).OrderByDescending(File.GetLastWriteTime))
        {
            using var file = File.OpenRead(trx);
            // Clears namespaces
            var doc = HtmlDocument.Load(file, new HtmlReaderSettings { CaseFolding = Sgml.CaseFolding.None });
            foreach (var result in doc.CssSelectElements("UnitTestResult"))
            {
                var id = result.Attribute("testId")!.Value;
                // Process only once per test id, this avoids duplicates when multiple trx files are processed
                if (ids.Add(id))
                {
                    var isFailed = result.Attribute("outcome")?.Value == "Failed";
                    var method = doc.CssSelectElement($"UnitTest[id={id}] TestMethod");
                    Debug.Assert(method != null);
                    // NOTE: we may have duplicate test FQN due to theories, which we'd run again in this case.
                    // Eventually, we might want to figure out how to filter theories in a cross-framework compatible 
                    // way, but for now, filtering by FQN should be enough, even if not 100% optimal.
                    var fqn = $"{method.Attribute("className")?.Value}.{method.Attribute("name")?.Value}";
                    if (!outcomes.TryGetValue(fqn, out var wasFailed) || !wasFailed)
                        // Only change the outcome if it was not already failed
                        outcomes[fqn] = isFailed;
                }
            }
        }

        return outcomes;
    }

    async Task<BufferedCommandResult> RunTestsAsync(string dotnet, List<string> args, IEnumerable<string> failed, IProgress<string> progress)
    {
        var testArgs = string.Join(" ", args);
        var finalArgs = args;
        var filter = string.Join('|', failed.Select(failed => $"FullyQualifiedName~{failed}"));
        if (filter.Length > 0)
        {
            testArgs = $"--filter \"{filter}\" {testArgs}";
            finalArgs.InsertRange(0, ["--filter", filter]);
        }

        finalArgs.Insert(0, "test");

        var result = await Cli.Wrap(dotnet)
            .WithArguments(finalArgs)
            .WithWorkingDirectory(Directory.GetCurrentDirectory())
            .WithValidation(CommandResultValidation.None)
            .WithStandardOutputPipe(PipeTarget.ToDelegate(progress.Report))
            .ExecuteBufferedAsync();

        return result;
    }

    [GeneratedRegex(":.*VSTEST.*:")]
    private static partial Regex HasTestExpr();

    [GeneratedRegex("Failed:.*Passed:.*Skipped:.*Total:.*")]
    private static partial Regex HasTestSummaryExpr();

    public class RetestSettings : CommandSettings
    {
        [Description("Maximum attempts to run tests")]
        [CommandOption("--attempts")]
        [DefaultValue(5)]
        public int Attempts { get; init; } = 5;

        #region trx

        [Description("Include test output in report")]
        [CommandOption("--output")]
        [DefaultValue(false)]
        public bool Output { get; init; }

        /// <summary>
        /// Whether to include skipped tests in the output.
        /// </summary>
        [Description("Include skipped tests in report")]
        [CommandOption("--skipped")]
        [DefaultValue(true)]
        public bool Skipped { get; init; } = true;

        /// <summary>
        /// Report as GitHub PR comment.
        /// </summary>
        [Description("Report as GitHub PR comment")]
        [CommandOption("--gh-comment")]
        [DefaultValue(true)]
        public bool GitHubComment { get; init; } = true;

        /// <summary>
        /// Report as GitHub PR comment.
        /// </summary>
        [Description("Report as GitHub step summary")]
        [CommandOption("--gh-summary")]
        [DefaultValue(true)]
        public bool GitHubSummary { get; init; } = true;

        #endregion
    }

    class MultilineTaskDescriptionColumn : ProgressColumn
    {
        public override IRenderable Render(RenderOptions options, ProgressTask task, TimeSpan deltaTime)
        {
            return new Markup(task.Description ?? string.Empty)
                .Overflow(Overflow.Ellipsis)
                .Justify(Justify.Left);
        }
    }
}
