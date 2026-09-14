# Cogito.Extensions.Logging.Serilog.Autofac

Wires `Microsoft.Extensions.Logging` to Serilog inside an Autofac container.

## Why

The combination you most often want: components inject `ILogger<T>`, the output lands in Serilog, and
neither the components nor the container configuration mention Serilog.

## Install

```shell
dotnet add package Cogito.Extensions.Logging.Serilog.Autofac
```

## Use

```csharp
builder.RegisterAllAssemblyModules();
```

The module registers the Serilog-backed `ILoggerFactory`, so `ILogger<T>` resolves and writes to the
Serilog logger registered in the container. Pair with `Cogito.Serilog.Autofac` to configure that
logger from the container as well.

## License

MIT.
