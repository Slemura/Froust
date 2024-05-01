using System;
using JetBrains.Annotations;
using VContainer;

namespace RpDev.Services.GenericFactories.VContainer
{
    [UsedImplicitly]
    public class PlainClassPlainClassFactory : IPlainClassFactory
    {
        private readonly IObjectResolver _resolver;

        public PlainClassPlainClassFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Create<T>(params object[] extraArgs) where T : class
        {
            var type = typeof(T);

            if (type.IsAbstract)
                throw new ArgumentException($"Abstract class '{type}' cannot be instantiated.");

            if (type.IsInterface)
                throw new ArgumentException($"Interface '{type}' cannot be instantiated.");

            var product = (T)Activator.CreateInstance(type, extraArgs);

            _resolver?.Inject(product);

            return product;
        }

        public object Create(Type type, params object[] extraArgs)
        {
            if (type.IsAbstract)
                throw new ArgumentException($"Abstract class '{type}' cannot be instantiated.");

            if (type.IsInterface)
                throw new ArgumentException($"Interface '{type}' cannot be instantiated.");

            var product = Activator.CreateInstance(type, extraArgs);

            _resolver?.Inject(product);

            return product;
        }
    }
}