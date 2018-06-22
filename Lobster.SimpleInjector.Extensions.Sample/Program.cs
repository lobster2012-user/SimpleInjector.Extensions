using System;
using System.Reflection.Metadata;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using DryIoc;
using static DryIoc.Setup;

namespace Lobster.SimpleInjector.Extensions.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            GC.Collect();
            Console.WriteLine("Hello World!");
            System.Threading.Thread.Sleep(-1);
        }

        static void Test()
        {
            var dry = new DryIoc.Container();
            dry.Register<IMetricSubmitter, EnrichMetricsDecorator>(
                setup: new DecoratorSetup(z=>
                {
                    return z.Parent.ImplementationType == typeof(SingleWorker);
                }, order : 0, useDecorateeReuse : false));
            dry.Register<IMetricSubmitter, DefaultMetricSubmitter>(Reuse.Singleton);
            dry.Register<SingleWorker>(Reuse.Singleton);
            dry.Register<ScopedWorker>(Reuse.Singleton);
            var worker = dry.Resolve<SingleWorker>();
            Console.WriteLine(worker);
            var worker2 = dry.Resolve<ScopedWorker>();
            Console.WriteLine(worker2);


            var  container = new global::SimpleInjector.Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.ForConsumer<BConsumer>()
                     .RegisterSingleton<AInterface, BImplementation>();

            container.ForConsumer<AConsumer>()
                     .Register<AInterface, AImplementation>(Lifestyle.Singleton);

            container.RegisterSingleton<AConsumer>();

            container.RegisterDecorator(
                typeof(AInterface), typeof(AInterfaceDecorator), Lifestyle.Singleton,
                z =>
                {
                    return true;
                });
            container.RegisterDecorator(
                typeof(AInterface), typeof(BInterfaceDecorator), Lifestyle.Singleton,
                z =>
                {
                    return true;
                });


            container.Register<SingleWorker>(Lifestyle.Singleton);
            container.Register<ScopedWorker>(Lifestyle.Scoped);

            container.RegisterDecorator<IMetricSubmitter, EnrichMetricsDecorator>();
            container.RegisterConditional<IMetricSubmitter, ProxyMetricSubmitter>(
                Lifestyle.Singleton,
                z => z.Consumer.ImplementationType == typeof(SingleWorker));

          

            container.Verify();

            container.GetInstance<SingleWorker>();
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                container.GetInstance<ScopedWorker>();

            }

            container.GetInstance<AConsumer>();
            container.GetInstance<AConsumer>();
            container.GetInstance<BConsumer>();
            container.GetInstance<BConsumer>();



        }
    }
    public class ProxyMetricSubmitter : IMetricSubmitter
    {
        private readonly IMetricSubmitter _handler;

        public ProxyMetricSubmitter(IMetricSubmitter handler)
        {
            _handler = handler;
        }
    }

    public interface IWorker
    {

    }
    public interface IWorker<T> : IWorker
    {

    }

    public class ScopedWorker : IWorker<ScopedWorker>
    {
        private readonly IMetricSubmitter _handler;

        public ScopedWorker(IMetricSubmitter handler)
        {
            _handler = handler;
            Console.WriteLine("{0} {1}", this.GetType().Name, handler.GetType().Name);
        }
    }
    public class SingleWorker : IWorker<SingleWorker>
    {
        private readonly IMetricSubmitter _handler;

        public SingleWorker(IMetricSubmitter handler)
        {
            _handler = handler;
            Console.WriteLine("{0} {1}", this.GetType().Name, handler.GetType().Name);

        }
    }

    public class EnrichMetricsDecorator : IMetricSubmitter
    {
        private readonly IMetricSubmitter _handler;

        public EnrichMetricsDecorator(IMetricSubmitter handler)
        {
            _handler = handler;
        }
    }
    public class DefaultMetricSubmitter : IMetricSubmitter
    {

    }
    public interface IMetricSubmitter<T>
    {

    }
    public interface IMetricSubmitter
    {

    }

    public class AInterfaceDecorator : AInterface
    {
        private readonly AInterface _handler;

        public AInterfaceDecorator(AInterface handler)
        {
            _handler = handler;
        }

        public void Method()
        {
            Console.WriteLine("decorator-a");
            _handler.Method();
        }
    }
    public class BInterfaceDecorator : AInterface
    {
        private readonly AInterface _handler;

        public BInterfaceDecorator(AInterface handler)
        {
            _handler = handler;
        }

        public void Method()
        {
            Console.WriteLine("decorator-b");
            _handler.Method();
        }
    }
    public interface AInterface2
    {

    }
    public class AImplementation2 : AInterface2
    {

    }
    public class BImplementation2 : AInterface2
    {

    }

    public interface AInterface
    {
        void Method();
    }
    public class AImplementation : AInterface
    {
        public void Method()
        {

        }
    }
    public class BImplementation : AInterface
    {
        public void Method()
        {

        }
    }
    public class AConsumer
    {
        public AConsumer(AInterface obj)
        {
            Console.WriteLine("{0} {1} {2}", this.GetHashCode(), this.GetType().Name, obj.GetType().Name);
            obj.Method();
        }
    }
    public class BConsumer
    {
        public BConsumer(AInterface obj)
        {
            Console.WriteLine("{0} {1} {2}", this.GetHashCode(), this.GetType().Name, obj.GetType().Name);
            obj.Method();
        }
    }
}
