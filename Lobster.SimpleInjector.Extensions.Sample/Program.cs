using System;
using SimpleInjector;

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
            Container container = new Container();

            container.ForConsumer<BConsumer>()
                     .RegisterSingleton<AInterface, BImplementation>();

            container.ForConsumer<AConsumer>()
                     .Register<AInterface, AImplementation>(Lifestyle.Singleton);

            container.RegisterSingleton<AConsumer>();
            container.Verify();

            container.GetInstance<AConsumer>();
            container.GetInstance<AConsumer>();
            container.GetInstance<BConsumer>();
            container.GetInstance<BConsumer>();
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

    }
    public class AImplementation : AInterface
    {

    }
    public class BImplementation : AInterface
    {

    }
    public class AConsumer
    {
        public AConsumer(AInterface obj)
        {
            Console.WriteLine("{0} {1} {2}", this.GetHashCode(), this.GetType().Name, obj.GetType().Name);
        }
    }
    public class BConsumer
    {
        public BConsumer(AInterface obj)
        {
            Console.WriteLine("{0} {1} {2}", this.GetHashCode(), this.GetType().Name, obj.GetType().Name);
        }
    }
}
