using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
    [SerializeField] private List<PoolInitializeData> _poolDatas = new List<PoolInitializeData>();
    [SerializeField] private Transform _parentForPool;

    private void Start()
    {
        PoolManager.InitParent(_parentForPool);
    }

    [ContextMenu("Prepare Pool")]
    private void PreparePool()
    {
        PoolManager.InitParent(_parentForPool);
        foreach (var poolInitializeData in _poolDatas)
        {
            PoolManager.PreparePoolObject(poolInitializeData.Prefab);
        }
    }
}

[System.Serializable]
public class PoolInitializeData
{
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab => _prefab;
}

