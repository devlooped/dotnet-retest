![Icon](assets/32.png) dotnet retest
============

[![Version](https://img.shields.io/nuget/v/dotnet-retest?color=royalblue)](https://www.nuget.org/packages/dotnet-retest)
[![Downloads](https://img.shields.io/nuget/dt/dotnet-retest)](https://www.nuget.org/packages/dotnet-retest)
[![License](https://img.shields.io/github/license/devlooped/dotnet-retest?color=blue)](https://github.com//devlooped/dotnet-retest/blob/main/license.txt)
[![Build](https://img.shields.io/github/actions/workflow/status/devlooped/dotnet-retest/build.yml?branch=main)](https://github.com/devlooped/dotnet-retest/actions)

<!-- #content -->
Runs `dotnet test` with retries for failed tests automatically, and pretty-prints aggregated 
test results, integrating also with GitHub PR comments just like [dotnet-trx](https://github.com/devlooped/dotnet-trx).

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/ciretry.png)

When running locally, it provides live progress on each run:

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/progress.png)

and timing and outcome for each attempt:

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/timings.png)

Typical usage: `dotnet retest [OPTIONS] [-- [dotnet test options]]` (with optional `--attempts` which defaults to `3`):

```yml
    - name: 🧪 test
      run: |
        dotnet tool update -g dotnet-retest
        dotnet retest -- --no-build [other test options and args]
```

PR comment integration:

![PR comment](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/comment.png)

> NOTE: this behavior is triggered by the presence of the `GITHUB_REF_NAME` and `CI` environment variables.

<!-- include src/dotnet-retest/help.md -->
```shell
USAGE:
    dotnet retest [OPTIONS] [-- [dotnet test options]]

OPTIONS:
                        DEFAULT                                                 
    -h, --help                     Prints help information                      
        --version                  Prints version information                   
        --retries       3          Maximum retries when re-running failed tests 
        --no-summary               Whether to emit a summary to console/GitHub  
        --output                   Include test output in report                
    -v, --verbosity     Quiet      Output display verbosity:                    
                                   - quiet: only failed tests are displayed     
                                   - normal: failed and skipped tests are       
                                   displayed                                    
                                   - verbose: failed, skipped and passed tests  
                                   are displayed                                
        --gh-comment    True       Report as GitHub PR comment                  
        --gh-summary    True       Report as GitHub step summary                
```

<!-- src/dotnet-retest/help.md -->

> NOTE: rendering the passed tests requires `verbose` verbosity, since typically 
> you'll just want to see the failed tests in the report, especially in projects with 
> large number of tests.

Install:

```shell
dotnet tool install -g dotnet-retest
```

Update:

```shell
dotnet tool update -g dotnet-retest
```

<!-- #content -->

## Open Source Maintenance Fee

To ensure the long-term sustainability of this project, use of dotnet-retest requires an 
[Open Source Maintenance Fee](https://opensourcemaintenancefee.org). While the source 
code is freely available under the terms of the [MIT License](https://github.com/devlooped/dotnet-retest/blob/main/license.txt), all other aspects of the 
project --including opening or commenting on issues, participating in discussions and 
downloading releases-- require [adherence to the Maintenance Fee](https://github.com/devlooped/dotnet-retest/blob/main/osmfeula.txt).

In short, if you use this project to generate revenue, the [Maintenance Fee is required](https://github.com/devlooped/dotnet-retest/blob/main/osmfeula.txt).

To pay the Maintenance Fee, [become a Sponsor](https://github.com/sponsors/devlooped) at the corresponding OSMF tier (starting at just $10!).

<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->
# Sponsors 

<!-- sponsors.md -->
[![Clarius Org](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/clarius.png "Clarius Org")](https://github.com/clarius)
[![MFB Technologies, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/MFB-Technologies-Inc.png "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![DRIVE.NET, Inc.](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/drivenet.png "DRIVE.NET, Inc.")](https://github.com/drivenet)
[![Keith Pickford](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Keflon.png "Keith Pickford")](https://github.com/Keflon)
[![Thomas Bolon](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/tbolon.png "Thomas Bolon")](https://github.com/tbolon)
[![Kori Francis](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/kfrancis.png "Kori Francis")](https://github.com/kfrancis)
[![Toni Wenzel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/twenzel.png "Toni Wenzel")](https://github.com/twenzel)
[![Uno Platform](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/unoplatform.png "Uno Platform")](https://github.com/unoplatform)
[![Reuben Swartz](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/rbnswartz.png "Reuben Swartz")](https://github.com/rbnswartz)
[![Jacob Foshee](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jfoshee.png "Jacob Foshee")](https://github.com/jfoshee)
[![](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Mrxx99.png "")](https://github.com/Mrxx99)
[![Eric Johnson](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/eajhnsn1.png "Eric Johnson")](https://github.com/eajhnsn1)
[![David JENNI](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/davidjenni.png "David JENNI")](https://github.com/davidjenni)
[![Jonathan ](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Jonathan-Hickey.png "Jonathan ")](https://github.com/Jonathan-Hickey)
[![Charley Wu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/akunzai.png "Charley Wu")](https://github.com/akunzai)
[![Ken Bonny](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/KenBonny.png "Ken Bonny")](https://github.com/KenBonny)
[![Simon Cropp](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/SimonCropp.png "Simon Cropp")](https://github.com/SimonCropp)
[![agileworks-eu](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/agileworks-eu.png "agileworks-eu")](https://github.com/agileworks-eu)
[![Zheyu Shen](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/arsdragonfly.png "Zheyu Shen")](https://github.com/arsdragonfly)
[![Vezel](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/vezel-dev.png "Vezel")](https://github.com/vezel-dev)
[![ChilliCream](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/ChilliCream.png "ChilliCream")](https://github.com/ChilliCream)
[![4OTC](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/4OTC.png "4OTC")](https://github.com/4OTC)
[![Vincent Limo](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/v-limo.png "Vincent Limo")](https://github.com/v-limo)
[![Jordan S. Jones](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jordansjones.png "Jordan S. Jones")](https://github.com/jordansjones)
[![domischell](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/DominicSchell.png "domischell")](https://github.com/DominicSchell)
[![Justin Wendlandt](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/jwendl.png "Justin Wendlandt")](https://github.com/jwendl)
[![Adrian Alonso](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/adalon.png "Adrian Alonso")](https://github.com/adalon)
[![Michael Hagedorn](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/Eule02.png "Michael Hagedorn")](https://github.com/Eule02)
[![torutek](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/torutek.png "torutek")](https://github.com/torutek)
[![Ryan McCaffery](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/mccaffers.png "Ryan McCaffery")](https://github.com/mccaffers)
[![Alex Wiese](https://raw.githubusercontent.com/devlooped/sponsors/main/.github/avatars/alexwiese.png "Alex Wiese")](https://github.com/alexwiese)


<!-- sponsors.md -->

[![Sponsor this project](https://raw.githubusercontent.com/devlooped/sponsors/main/sponsor.png "Sponsor this project")](https://github.com/sponsors/devlooped)
&nbsp;

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
