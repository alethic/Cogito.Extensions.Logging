# Cogito.Extensions.Logging.Autofac

Makes `ILogger` and `ILogger<T>` resolvable from an Autofac container.

## Why

`ILogger<T>` depends on the type it is injected into, which a container has to special-case. Without
that, every component either takes `ILoggerFactory` and creates its own, or you register a logger per
type by hand.

## Install

```shell
dotnet add package Cogito.Extensions.Logging.Autofac
```

## Use

The assembly module registers itself, so with module scanning there is nothing to call:

```csharp
builder.RegisterAllAssemblyModules();
```

After which any component can take a logger:

```csharp
public class Importer
{
    public Importer(ILogger<Importer> logger) { ... }
}
```

`ILogger<T>` is resolved for the consuming type, and a non-generic `ILogger` is available too.

## License

MIT.
