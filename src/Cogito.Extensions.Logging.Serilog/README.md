# Cogito.Extensions.Logging.Serilog

Routes `Microsoft.Extensions.Logging` output into Serilog.

## Why

Libraries log through `ILogger`; you want the output in Serilog with its sinks and structured
properties intact, without every library taking a Serilog dependency.

## Install

```shell
dotnet add package Cogito.Extensions.Logging.Serilog
```

## Use

```csharp
var factory = new SerilogLoggerFactory(Log.Logger);
var logger = factory.CreateLogger<Importer>();
```

Message templates and their named properties are passed through to Serilog rather than being
formatted first, so structured logging survives the hop.

## License

MIT.
