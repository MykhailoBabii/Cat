using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.DI
{
    public enum ServiceLifetime
    {
        Singleton,
        Transient
    }

    public class DiServiceCollection 
    {
        private Dictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

        public void RegisterSingleton<TService>()
        {
            _serviceDescriptors[typeof(TService)] = new ServiceDescriptor(typeof(TService), ServiceLifetime.Singleton);
        }

        public void RegisterSingleton<TService>(TService implementation)
        {
            _serviceDescriptors[typeof(TService)] = new ServiceDescriptor(implementation, ServiceLifetime.Singleton);
        }

        public void RegisterTransient<TService>()
        {
            _serviceDescriptors[typeof(TService)] = new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient);
        }

        public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
        {
            _serviceDescriptors[typeof(TService)] = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _serviceDescriptors[typeof(TService)] = new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);
        }

        public DiContainer GenerateContainer()
        {
            return new DiContainer(_serviceDescriptors);
        }
    }

    public class DiContainer
    {
        private Dictionary<Type, ServiceDescriptor> _serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

        public DiContainer(Dictionary<Type, ServiceDescriptor> serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }

        public object GetService(Type service)
        {
            if (_serviceDescriptors.ContainsKey(service) == false)
            {
                throw new Exception($"Service {service.Name} is not registered");
            }

            var descriptor = _serviceDescriptors[service];
            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            var actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
            {
                throw new Exception($"Cannot instantiate abstract classes or interfaces of {service.Name} is not registered");
            }

            var constructorInfo = actualType.GetConstructors().First();

            var parameters = constructorInfo.GetParameters()
                .Select(x => GetService(x.ParameterType)).ToArray();

            var implementation = Activator.CreateInstance(actualType, parameters);

            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {
                descriptor.SetupImplementation(implementation);
            }

            return implementation;
        }

        public T GetService<T>()
        {

            return (T)GetService(typeof(T));
            
            //return default;
        }
    }

    public class ServiceDescriptor
    {
        public Type ServiceType { get;} 
        public Type ImplementationType { get; }
        public object Implementation { get; private set; }

        public ServiceLifetime Lifetime { get; }
        public ServiceDescriptor(object implementation, ServiceLifetime lifetime)
        {
            ServiceType = implementation.GetType();
            Implementation = implementation;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        internal void SetupImplementation<T>(T implementation)
        {
            Implementation = implementation;
        }
    }

    
}