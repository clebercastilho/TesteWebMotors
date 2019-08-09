using System;
using WebMotors.Test.Domain.Interfaces;

namespace WebMotors.Test.Domain.Events
{
    public static class DomainEvent
    {
        public static Func<IServiceProvider> ContainerAccessor { get; set; }
        private static IServiceProvider Container => ContainerAccessor();

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (Container == null) return;

            var obj = Container.GetService(typeof(IHandler<T>));
            ((IHandler<T>)obj).Handle(args);
        }
    }
}
