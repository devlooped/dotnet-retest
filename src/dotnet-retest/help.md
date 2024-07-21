```shell
USAGE:
    dotnet retest [OPTIONS] [-- [dotnet test options]]

OPTIONS:
                        DEFAULT                                   
    -h, --help                     Prints help information        
    -v, --version                  Prints version information     
        --attempts      5          Maximum attempts to run tests  
        --output                   Include test output in report  
        --skipped       True       Include skipped tests in report
        --gh-comment    True       Report as GitHub PR comment    
        --gh-summary    True       Report as GitHub step summary  
```
