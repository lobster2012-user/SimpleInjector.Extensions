# SimpleInjector.Extensions
Fluent extensions for SimpleInjector ( pull requests accepted )

[![NuGet](https://img.shields.io/nuget/v/Lobster.SimpleInjector.Extensions.svg)](https://www.nuget.org/packages/Lobster.SimpleInjector.Extensions)
[![NuGet](https://img.shields.io/nuget/dt/Lobster.SimpleInjector.Extensions.svg?colorB=FF00FF)](https://www.nuget.org/packages/Lobster.SimpleInjector.Extensions)
[![Build status](https://ci.appveyor.com/api/projects/status/irp8vllgf6q7ld0g?svg=true)](https://ci.appveyor.com/project/lobster2012-user/simpleinjector-extensions)
[![Build status](https://travis-ci.org/lobster2012-user/SimpleInjector.Extensions.svg?branch=master)](https://travis-ci.org/lobster2012-user/SimpleInjector.Extensions)

```csharp
container.ForConsumer<AConsumer>()
         .Register<AInterface, AImplementation>(Lifestyle.Singleton);
```
