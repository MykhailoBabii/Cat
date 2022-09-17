using Core.DISimple;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Helpers
{
    public enum ObjectType
    {
        GameObject,
        Transform,
        RectTransform
    }

    public class DynamicObjectRegister : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private ObjectType _objectType;

        private void Start()
        {
            RegisterObject();
        }

        private void OnDestroy()
        {
            var diContainer = ServiceLocator.Resolve<DI.DiContainer>();
            var objectHolder = diContainer.GetService<IDynamicObjectHolder>();
            objectHolder.Unregister(_key);
        }

        private void RegisterObject()
        {
            var diContainer = ServiceLocator.Resolve<DI.DiContainer>();
            var objectHolder = diContainer.GetService<IDynamicObjectHolder>();
            switch (_objectType)
            {
                case ObjectType.GameObject:
                    objectHolder.Register<GameObject>(_key, gameObject);
                    break;
                case ObjectType.Transform:
                    objectHolder.Register<Transform>(_key, transform);
                    break;
                case ObjectType.RectTransform:
                    objectHolder.Register<RectTransform>(_key, GetComponent<RectTransform>());
                    break;
                default:
                    break;
            }
        }
    }
}