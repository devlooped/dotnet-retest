![Icon](assets/32.png) dotnet retest
============

[![Version](https://img.shields.io/nuget/v/retest?color=royalblue)](https://www.nuget.org/packages/retest)
[![Downloads](https://img.shields.io/nuget/dt/retest)](https://www.nuget.org/packages/retest)
[![License](https://img.shields.io/github/license/devlooped/dotnet-retest?color=blue)](https://github.com//devlooped/dotnet-retest/blob/main/license.txt)
[![Build](https://img.shields.io/github/actions/workflow/status/devlooped/dotnet-retest/build.yml?branch=main)](https://github.com/devlooped/dotnet-retest/actions)

<!-- #content -->
Runs `dotnet test` with retries for failed tests automatically, and pretty-prints aggregated 
test results, integrating also with GitHub PR comments just like [dotnet-trx](https://github.com/devlooped/dotnet-trx).

Install latest and run with a single command: `dnx retest`.

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/ciretry.png)

When running locally, it provides live progress on each run:

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/progress.png)

and timing and outcome for each attempt:

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/timings.png)

Typical usage: `dotnet retest [OPTIONS] [-- [dotnet test options]]`:

```yml
    - name: 🧪 test
      run: |
        dotnet tool update -g dotnet-retest
        dotnet retest -- --no-build [other test options and args]
```

PR comment integration:

![PR comment](https://raw.githubusercontent.com/devlooped/dotnet-retest/main/assets/img/comment.png)

> NOTE: this behavior is triggered by the presence of the `GITHUB_REF_NAME` and `CI` environment variables.

<!-- include src/Retest/help.md -->
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

<!-- src/Retest/help.md -->

> NOTE: rendering the passed tests requires `verbose` verbosity, since typically 
> you'll just want to see the failed tests in the report, especially in projects with 
> large number of tests.

Install and Run latest:

```shell
dnx retest
```

Install:

```shell
dotnet tool install -g retest
```

Update:

```shell
dotnet tool update -g retest
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
[![Clarius Org](https://avatars.githubusercontent.com/u/71888636?v=4&s=39 "Clarius Org")](https://github.com/clarius)
[![MFB Technologies, Inc.](https://avatars.githubusercontent.com/u/87181630?v=4&s=39 "MFB Technologies, Inc.")](https://github.com/MFB-Technologies-Inc)
[![SandRock](https://avatars.githubusercontent.com/u/321868?u=99e50a714276c43ae820632f1da88cb71632ec97&v=4&s=39 "SandRock")](https://github.com/sandrock)
[![DRIVE.NET, Inc.](https://avatars.githubusercontent.com/u/15047123?v=4&s=39 "DRIVE.NET, Inc.")](https://github.com/drivenet)
[![Keith Pickford](https://avatars.githubusercontent.com/u/16598898?u=64416b80caf7092a885f60bb31612270bffc9598&v=4&s=39 "Keith Pickford")](https://github.com/Keflon)
[![Thomas Bolon](https://avatars.githubusercontent.com/u/127185?u=7f50babfc888675e37feb80851a4e9708f573386&v=4&s=39 "Thomas Bolon")](https://github.com/tbolon)
[![Kori Francis](https://avatars.githubusercontent.com/u/67574?u=3991fb983e1c399edf39aebc00a9f9cd425703bd&v=4&s=39 "Kori Francis")](https://github.com/kfrancis)
[![Uno Platform](https://avatars.githubusercontent.com/u/52228309?v=4&s=39 "Uno Platform")](https://github.com/unoplatform)
[![Reuben Swartz](https://avatars.githubusercontent.com/u/724704?u=2076fe336f9f6ad678009f1595cbea434b0c5a41&v=4&s=39 "Reuben Swartz")](https://github.com/rbnswartz)
[![Jacob Foshee](https://avatars.githubusercontent.com/u/480334?v=4&s=39 "Jacob Foshee")](https://github.com/jfoshee)
[![](https://avatars.githubusercontent.com/u/33566379?u=bf62e2b46435a267fa246a64537870fd2449410f&v=4&s=39 "")](https://github.com/Mrxx99)
[![Eric Johnson](https://avatars.githubusercontent.com/u/26369281?u=41b560c2bc493149b32d384b960e0948c78767ab&v=4&s=39 "Eric Johnson")](https://github.com/eajhnsn1)
[![David JENNI](https://avatars.githubusercontent.com/u/3200210?v=4&s=39 "David JENNI")](https://github.com/davidjenni)
[![Jonathan ](https://avatars.githubusercontent.com/u/5510103?u=98dcfbef3f32de629d30f1f418a095bf09e14891&v=4&s=39 "Jonathan ")](https://github.com/Jonathan-Hickey)
[![Charley Wu](https://avatars.githubusercontent.com/u/574719?u=ea7c743490c83e8e4b36af76000f2c71f75d636e&v=4&s=39 "Charley Wu")](https://github.com/akunzai)
[![Ken Bonny](https://avatars.githubusercontent.com/u/6417376?u=569af445b6f387917029ffb5129e9cf9f6f68421&v=4&s=39 "Ken Bonny")](https://github.com/KenBonny)
[![Simon Cropp](https://avatars.githubusercontent.com/u/122666?v=4&s=39 "Simon Cropp")](https://github.com/SimonCropp)
[![agileworks-eu](https://avatars.githubusercontent.com/u/5989304?v=4&s=39 "agileworks-eu")](https://github.com/agileworks-eu)
[![Zheyu Shen](https://avatars.githubusercontent.com/u/4067473?v=4&s=39 "Zheyu Shen")](https://github.com/arsdragonfly)
[![Vezel](https://avatars.githubusercontent.com/u/87844133?v=4&s=39 "Vezel")](https://github.com/vezel-dev)
[![ChilliCream](https://avatars.githubusercontent.com/u/16239022?v=4&s=39 "ChilliCream")](https://github.com/ChilliCream)
[![4OTC](https://avatars.githubusercontent.com/u/68428092?v=4&s=39 "4OTC")](https://github.com/4OTC)
[![Vincent Limo](https://avatars.githubusercontent.com/devlooped-user?s=39 "Vincent Limo")](https://github.com/v-limo)
[![domischell](https://avatars.githubusercontent.com/u/66068846?u=0a5c5e2e7d90f15ea657bc660f175605935c5bea&v=4&s=39 "domischell")](https://github.com/DominicSchell)
[![Justin Wendlandt](https://avatars.githubusercontent.com/u/1068431?u=f7715ed6a8bf926d96ec286f0f1c65f94bf86928&v=4&s=39 "Justin Wendlandt")](https://github.com/jwendl)
[![Adrian Alonso](https://avatars.githubusercontent.com/u/2027083?u=129cf516d99f5cb2fd0f4a0787a069f3446b7522&v=4&s=39 "Adrian Alonso")](https://github.com/adalon)
[![Michael Hagedorn](https://avatars.githubusercontent.com/u/61711586?u=8f653dfcb641e8c18cc5f78692ebc6bb3a0c92be&v=4&s=39 "Michael Hagedorn")](https://github.com/Eule02)
[![](https://avatars.githubusercontent.com/devlooped-user?s=39 "")](https://github.com/henkmartijn)
[![torutek](https://avatars.githubusercontent.com/u/33917059?v=4&s=39 "torutek")](https://github.com/torutek)
[![mccaffers](https://avatars.githubusercontent.com/u/16667079?u=739e110e62a75870c981640447efa5eb2cb3bc8f&v=4&s=39 "mccaffers")](https://github.com/mccaffers)
[![ADS Fund](https://avatars.githubusercontent.com/u/202042116?v=4&s=39 "ADS Fund")](https://github.com/ADS-Fund)


<!-- sponsors.md -->
[![Sponsor this project](https://avatars.githubusercontent.com/devlooped-sponsor?s=118 "Sponsor this project")](https://github.com/sponsors/devlooped)

[Learn more about GitHub Sponsors](https://github.com/sponsors)

<!-- https://github.com/devlooped/sponsors/raw/main/footer.md -->
