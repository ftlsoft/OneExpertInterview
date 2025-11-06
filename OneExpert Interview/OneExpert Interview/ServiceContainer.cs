using OneExpertInterview.Application.Interfaces;
using OneExpertInterview.Application.Services;
using OneExpertInterview.Domain.Interfaces;
using OneExpertInterview.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneExpertInterview
{
    /// <summary>
    /// taken from https://medium.com/@bhargavkoya56/crafting-your-own-dependency-injection-container-in-c-a-developers-journey-bd255633dd5f
    /// only singleton lifetime is implemented for simplicity
    /// </summary>
  
    public interface IServiceContainer
    {
        void RegisterSingleton<TInterface, TImplementation>() where TImplementation : class, TInterface;

        T GetService<T>();
        object GetService(Type serviceType);
    }

    public class SimpleContainer : IServiceContainer, IDisposable
    {
        private readonly Dictionary<Type, ServiceDescriptor> _services = new();
        private readonly Dictionary<Type, object> _singletonInstances = new();

        public class ServiceDescriptor
        {
            public Type ServiceType { get; set; }
            public Type ImplementationType { get; set; }
            public ServiceLifetime Lifetime { get; set; }
            public object Instance { get; set; }
            public Func<IServiceContainer, object> Factory { get; set; }
        }
        
        public enum ServiceLifetime
        {
            Singleton
        }

        public void Dispose()
        {
            //if (!_disposed)
            //{
            //    foreach (var instance in _scopedInstances.Values)
            //    {
            //        if (instance is IDisposable disposable)
            //        {
            //            disposable.Dispose();
            //        }
            //    }
            //    _scopedInstances.Clear();
            //    _disposed = true;
            //}
        }

        private object GetSingleton(ServiceDescriptor serviceDescriptor)
        {
            if (_singletonInstances.TryGetValue(serviceDescriptor.ServiceType, out var instance))
            {
                return instance;
            }

            instance = CreateInstance(serviceDescriptor.ImplementationType);
            _singletonInstances[serviceDescriptor.ServiceType] = instance;
            return instance;
        }

        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : class, TInterface
        {
            _services[typeof(TInterface)] = new ServiceDescriptor
            {
                ServiceType = typeof(TInterface),
                ImplementationType = typeof(TImplementation),
                Lifetime = ServiceLifetime.Singleton
            };
        }
        
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            if (!_services.TryGetValue(serviceType, out var serviceDescriptor))
            {
                throw new InvalidOperationException($"Service of type {serviceType.Name} is not registered.");
            }

            return serviceDescriptor.Lifetime switch
            {
                ServiceLifetime.Singleton => GetSingleton(serviceDescriptor),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private object CreateInstance(Type implementationType)
        {
            var constructors = implementationType.GetConstructors();
            var constructor = constructors[0]; // Simplified: take first constructor

            var parameters = constructor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                args[i] = GetService(parameters[i].ParameterType);
            }

            return Activator.CreateInstance(implementationType, args);
        }
    }
}
