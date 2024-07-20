![Icon](assets/32.png) dotnet retest
============

[![Version](https://img.shields.io/nuget/vpre/dotnet-retest.svg?color=royalblue)](https://www.nuget.org/packages/dotnet-retest)
[![Downloads](https://img.shields.io/nuget/dt/dotnet-retest.svg?color=green)](https://www.nuget.org/packages/dotnet-retest)
[![License](https://img.shields.io/github/license/devlooped/dotnet-retest.svg?color=blue)](https://github.com//devlooped/dotnet-retest/blob/main/license.txt)
[![Build](https://github.com/devlooped/dotnet-retest/workflows/build/badge.svg?branch=main)](https://github.com/devlooped/dotnet-retest/actions)

<!-- #content -->
Runs `dotnet test` with retries for failed tests automatically, and pretty-prints aggregated 
test results, integrating also with GitHub PR comments just like [dotnet-trx](https://github.com/devlooped/dotnet-trx).

![Demo](https://raw.githubusercontent.com/devlooped/dotnet-trx/main/assets/img/demo.png)

![PR comment](https://raw.githubusercontent.com/devlooped/dotnet-trx/main/assets/img/comment.png)

Typical usage: `dotnet retest [OPTIONS] [-- [dotnet test options]]` (with optional `--attempts` which defaults to `5`):

```yml
    - name: 🧪 test
      run: |
        dotnet tool update -g dotnet-retest
        dotnet retest -- --no-build [other test options and args]
```

![PR comment](https://raw.githubusercontent.com/devlooped/dotnet-trx/main/assets/img/comment.png)

> NOTE: this behavior is triggered by the presence of the `GITHUB_REF_NAME` and `CI` environment variables.

<!-- include src/dotnet-retest/help.md -->

Install:

```shell
dotnet tool install -g dotnet-retest
```

Update:

```shell
dotnet tool update -g dotnet-retest
```

<!-- #content -->
<!-- include https://github.com/devlooped/sponsors/raw/main/footer.md -->