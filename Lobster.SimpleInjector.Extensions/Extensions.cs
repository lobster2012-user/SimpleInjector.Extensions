using System;
using System.Collections.Generic;
using System.Text;
using SimpleInjector;

namespace Lobster.SimpleInjector.Extensions
{
    public static class Extensions
    {
        public static ConsumerRegisterObject<TConsumer> ForConsumer<TConsumer>(
            this Container container, Predicate<PredicateContext> predicate = null)
        {
            return new ConsumerRegisterObject<TConsumer>(container, predicate);
        }
    }
}
