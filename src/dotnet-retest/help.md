```shell
USAGE:
    dotnet retest [OPTIONS] [-- [dotnet test options]]

OPTIONS:
                            DEFAULT                                   
    -h, --help                         Prints help information        
    -v, --version                      Prints version information     
        --attempts          5          Maximum attempts to run tests  
        --trx-output                   Include test output in report  
        --trx-skipped       True       Include skipped tests in report
        --trx-gh-comment    True       Report as GitHub PR comment    
        --trx-gh-summary    True       Report as GitHub step summary  
```
