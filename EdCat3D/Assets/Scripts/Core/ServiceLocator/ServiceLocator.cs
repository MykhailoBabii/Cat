using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DISimple
{

    public static class ServiceLocator
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<TService>(TService service)
        {
            if (_services.ContainsKey(typeof(TService)) == true)
            {
                Debug.LogError(string.Format("Service {0} already exist", typeof(TService).Name));
            }
            else
            {
                _services[typeof(TService)] = service;
            }
        }

        public static void Unregister<TService>()
        {
            if (_services.ContainsKey(typeof(TService)) == false)
            {
                Debug.LogError(string.Format("Service {0} did not exist", typeof(TService).Name));
            }
            else
            {
                _services.Remove(typeof(TService));
            }
        }

        public static bool IsServiceExist<TService>()
        {
            return _services.ContainsKey(typeof(TService));
        }

        public static TService Resolve<TService>()
        {
            if (_services.ContainsKey(typeof(TService)) == false)
            {
                Debug.LogError(string.Format("Service {0} did not exist", typeof(TService).Name));
                return default(TService);
            }
            else
            {
                return (TService)_services[typeof(TService)];
            }
        }
    }
}