# Cogito.Extensions.Logging

[![Build](https://github.com/alethic/Cogito.Extensions.Logging/actions/workflows/Cogito.Extensions.Logging.yml/badge.svg)](https://github.com/alethic/Cogito.Extensions.Logging/actions/workflows/Cogito.Extensions.Logging.yml)

Makes Microsoft.Extensions.Logging resolvable from Autofac and routes it to Serilog.

## Packages

**[Cogito.Extensions.Logging](https://www.nuget.org/packages/Cogito.Extensions.Logging)** — Shared logging primitives for the Cogito family, over `Microsoft.Extensions.Logging`.

**[Cogito.Extensions.Logging.Autofac](https://www.nuget.org/packages/Cogito.Extensions.Logging.Autofac)** — Makes `ILogger` and `ILogger<T>` resolvable from an Autofac container.

**[Cogito.Extensions.Logging.Serilog](https://www.nuget.org/packages/Cogito.Extensions.Logging.Serilog)** — Routes `Microsoft.Extensions.Logging` output into Serilog.

**[Cogito.Extensions.Logging.Serilog.Autofac](https://www.nuget.org/packages/Cogito.Extensions.Logging.Serilog.Autofac)** — Wires `Microsoft.Extensions.Logging` to Serilog inside an Autofac container.

Each package carries its own README with the detail; the links above go to nuget.org.

## Building

```shell
dotnet restore Cogito.Extensions.Logging.sln
dotnet msbuild -p:Configuration=Release Cogito.Extensions.Logging.dist.msbuildproj
```

Packages are staged into `dist/nuget` and test suites into `dist/tests`; run a suite with
`dotnet test -f <tfm> <path to its assembly>`.

## License

MIT — see [LICENSE](LICENSE).
