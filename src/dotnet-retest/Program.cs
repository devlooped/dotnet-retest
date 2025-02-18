using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Devlooped;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;
using Spectre.Console.Rendering;

var app = new CommandApp<RetestCommand>();

// Alias -? to -h for help
if (args.Contains("-?"))
    args = args.Select(x => x == "-?" ? "-h" : x).ToArray();

if (args.Contains("--debug"))
{
    Debugger.Launch();
    args = args.Where(args => args != "--debug").ToArray();
}

app.Configure(config =>
{
    config.SetHelpProvider(new Helper(config.Settings));
    config.SetApplicationName("dotnet retest");

    if (Environment.GetEnvironmentVariables().Contains("NO_COLOR") &&
        config.Settings.HelpProviderStyles?.Options is { } options)
        options.DefaultValue = Style.Plain;
});

if (args.Contains("--version"))
{
    AnsiConsole.MarkupLine($"{ThisAssembly.Project.ToolCommandName} version [lime]{ThisAssembly.Project.Version}[/] ({ThisAssembly.Project.BuildDate})");
    AnsiConsole.MarkupLine($"[link]{ThisAssembly.Git.Url}/releases/tag/{ThisAssembly.Project.BuildRef}[/]");

    foreach (var message in await CheckUpdates(args))
        AnsiConsole.MarkupLine(message);

    return 0;
}

var updates = Task.Run(() => CheckUpdates(args));
var exit = app.Run(args);

if (await updates is { Length: > 0 } messages)
{
    foreach (var message in messages)
        AnsiConsole.MarkupLine(message);
}

return exit;

static async Task<string[]> CheckUpdates(string[] args)
{
    if (args.Contains("-u") && !args.Contains("--unattended"))
        return [];

    var providers = Repository.Provider.GetCoreV3();
    var repository = new SourceRepository(new PackageSource("https://api.nuget.org/v3/index.json"), providers);
    var resource = await repository.GetResourceAsync<PackageMetadataResource>();
    var localVersion = new NuGetVersion(ThisAssembly.Project.Version);
    var metadata = await resource.GetMetadataAsync(ThisAssembly.Project.PackageId, true, false,
        new SourceCacheContext
        {
            NoCache = true,
            RefreshMemoryCache = true,
        },
        NuGet.Common.NullLogger.Instance, CancellationToken.None);

    var update = metadata
        .Select(x => x.Identity)
        .Where(x => x.Version > localVersion)
        .OrderByDescending(x => x.Version)
        .Select(x => x.Version)
        .FirstOrDefault();

    if (update != null)
    {
        return [
            $"There is a new version of [yellow]{ThisAssembly.Project.PackageId}[/]: [dim]v{localVersion.ToNormalizedString()}[/] -> [lime]v{update.ToNormalizedString()}[/]",
            $"Update with: [yellow]dotnet[/] tool update -g {ThisAssembly.Project.PackageId}"
        ];
    }

    return [];
}

class Helper(ICommandAppSettings settings) : HelpProvider(settings)
{
    const string dotnet = "[-- [dotnet test options]]";

    public override IEnumerable<IRenderable> GetUsage(ICommandModel model, ICommandInfo? command)
        => [new Markup(
            $"""
            [yellow]USAGE:[/]
                {settings.ApplicationName} [[OPTIONS]] [grey]{dotnet.EscapeMarkup()}[/]

            """)];
}
