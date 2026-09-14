# CLAUDE.md

## What this is

Cogito.Extensions.Logging — Various utilities for working with Microsoft.Extensions.Logging.

Publishes 4 packages: `Cogito.Extensions.Logging`, `Cogito.Extensions.Logging.Autofac`, `Cogito.Extensions.Logging.Serilog`, `Cogito.Extensions.Logging.Serilog.Autofac`.

## Build and test

```shell
dotnet restore Cogito.Extensions.Logging.sln
dotnet msbuild -p:Configuration=Release Cogito.Extensions.Logging.dist.msbuildproj
```

The dist project stages packages into `dist/nuget` and test suites into
`dist/tests/<suite>/<tfm>`; run a suite with `dotnet test -f <tfm> <path to its assembly>`.
A .NET Framework suite builds as an `.exe`, not a `.dll`.

## Conventions

- Packaging is the dist project's job. No project sets `GeneratePackageOnBuild`.
- Versions come from GitVersion. Release by creating a GitHub release; that tag publishes to nuget.org.
- Add package references with `dotnet add package`, no version, so the version resolved is one the project's target frameworks support. Don't hand-edit the csproj.
- Add target frameworks, never silently substitute one. Dropping a target framework is a breaking change.
- Every package carries its own `README.md`; the repository `README.md` is for GitHub.

## Attribution

Do not add AI or tool attribution anywhere — not in commit messages, pull request
descriptions, code comments, or documentation. No `Co-Authored-By` trailers for tools, no
"generated with" footers. Write commits and PRs as the author.
