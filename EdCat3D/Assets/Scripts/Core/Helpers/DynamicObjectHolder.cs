using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Helpers
{
    public interface IDynamicObjectHolder
    {
        void Register<T>(string key, T value);
        T GetObject<T>(string key);
        void Unregister(string key);
    }

    public class DynamicObjectHolder : IDynamicObjectHolder
    {
        private Dictionary<string, object> _dynamicObjects = new Dictionary<string, object>();

        public T GetObject<T>(string key)
        {
            if (_dynamicObjects.ContainsKey(key) == false)
            {
                Debug.LogError($"[{GetType().Name}][GetObject] Can't find object with key {key}");
                return default;
            }
            else
            {
                return (T)_dynamicObjects[key];
            }
        }

        public void Register<T>(string key, T value)
        {
            if (_dynamicObjects.ContainsKey(key))
            {
                Debug.LogError($"[{GetType().Name}][Register] object with key {key} already exist");
            }
            else
            {
                _dynamicObjects[key] = value;
            }
        }

        public void Unregister(string key)
        {
            if (_dynamicObjects.ContainsKey(key) == false)
            {
                Debug.LogError($"[{GetType().Name}][GetObject] Can't find object with key {key}");
            }
            else
            {
                _dynamicObjects.Remove(key);
            }
        }
    }
}