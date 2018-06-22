using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;

namespace Lobster.SimpleInjector
{
    public class ConsumerRegisterObject<TConsumer>
    {
        private readonly Predicate<PredicateContext> _predicate;
        internal Container Container { get; }

        bool Predicate(PredicateContext context)
        {
            if (context.Consumer.ImplementationType != typeof(TConsumer))
                return false;

            return _predicate != null ? _predicate(context) : true;
        }

        public ConsumerRegisterObject(Container container, Predicate<PredicateContext> predicate)
        {
            _predicate = predicate;
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public Container Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Container.RegisterConditional<TInterface, TImplementation>(Predicate);
            return Container;
        }
        public Container RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return Register<TInterface, TImplementation>(Lifestyle.Singleton);
        }
        public Container Register<TInterface, TImplementation>(Lifestyle lifeStyle)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Container.RegisterConditional<TInterface, TImplementation>(lifeStyle, Predicate);
            return Container;
        }
    }
}
