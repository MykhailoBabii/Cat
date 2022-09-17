using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ResourceInner
{

    [CreateAssetMenu(fileName = "ResourceProvider", menuName = "Create/Resource Provider")]
    public class ResourceProvider : ScriptableObject
    {
        private const string SpriteResourcePathFormat = "Sprites/";
        private const string AudioResourcePathFormat = "AudioClips/";
        private const string RenderTexturesResourcePathFormat = "RenderTextures/";
        private const string PrefabResourcePathFormat = "Prefabs/";
        
        [SerializeField] private List<ResourceDescriptor> _resources = new List<ResourceDescriptor>();

        public Sprite GetSprite(string nameId)
        {
            return GetResource<Sprite>(ResourceType.Sprite, nameId);
        }

        public AudioClip GetAudio(string nameId)
        {
            return GetResource<AudioClip>(ResourceType.Audio, nameId);
        }

        public RenderTexture GetRenderTexture(string nameId)
        {
            return GetResource<RenderTexture>(ResourceType.RenderTexture, nameId);
        }

        public GameObject GetPrefab(string nameId)
        {
            return GetResource<GameObject>(ResourceType.GameObject, nameId);
        }

        private void OnEnable()
        {
            
        }

        private T GetResource<T>(ResourceType resourceType, string nameId) where T : Object
        {
            var descriptor = GetResourceDescriptor(resourceType, nameId);
            if (descriptor != null)
            {
                var path = $"{GetResourcePath(resourceType)}{descriptor.Path}";
                var result = Resources.Load<T>(path);
                return result;
            }
            Debug.LogError($"[{GetType().Name}][GetResource]Can't find resource with id: {nameId}");
            return null;
        }

        private string GetResourcePath(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Sprite:
                    return SpriteResourcePathFormat;
                case ResourceType.GameObject:
                    return PrefabResourcePathFormat;
                case ResourceType.Audio:
                    return AudioResourcePathFormat;
                case ResourceType.RenderTexture:
                    return RenderTexturesResourcePathFormat;
                default:
                    return SpriteResourcePathFormat;
            }
        }

        private ResourceDescriptor GetResourceDescriptor(ResourceType resourceType, string nameId)
        {
            var result = _resources.Find(resource => resource.Type == resourceType && resource.Id == nameId);
            return result;
        }
    }

    public enum ResourceType
    {
        Sprite,
        GameObject,
        Audio,
        RenderTexture
    }

    [System.Serializable]
    public class ResourceDescriptor
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private string _id;
        [SerializeField] private string _path;

        public ResourceType Type => _resourceType;
        public string Id => _id;
        public string Path => _path;
    }

}