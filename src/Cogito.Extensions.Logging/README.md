# Cogito.Extensions.Logging

Shared logging primitives for the Cogito family, over `Microsoft.Extensions.Logging`.

## Why

The abstraction packages in this family need a common place to depend on logging without each of them
choosing a logging implementation. This is that package — take it when you want the family's logging
types; take one of the integration packages below when you want logging actually wired up.

## Install

```shell
dotnet add package Cogito.Extensions.Logging
```

## See also

- `Cogito.Extensions.Logging.Autofac` — makes `ILogger<T>` resolvable from an Autofac container.
- `Cogito.Extensions.Logging.Serilog` — routes `Microsoft.Extensions.Logging` output to Serilog.
- `Cogito.Extensions.Logging.Serilog.Autofac` — both of the above together.

## License

MIT.
