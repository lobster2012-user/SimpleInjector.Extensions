# SimpleInjector.Extensions
SimpleInjector.Extensions

[![NuGet](https://img.shields.io/nuget/v/Lobster.SimpleInjector.Extensions.svg)](https://www.nuget.org/packages/Lobster.SimpleInjector.Extensions)
[![Build status](https://ci.appveyor.com/api/projects/status/irp8vllgf6q7ld0g?svg=true)](https://ci.appveyor.com/project/lobster2012-user/simpleinjector-extensions)


```csharp
container.ForConsumer<AConsumer>()
         .Register<AInterface, AImplementation>(Lifestyle.Singleton);
```
