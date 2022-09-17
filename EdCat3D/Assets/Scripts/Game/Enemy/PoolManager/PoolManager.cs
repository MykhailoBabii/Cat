using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PoolManager
{
    private static Dictionary<string, LinkedList<GameObject>> _pool = new Dictionary<string, LinkedList<GameObject>>();
    private static Transform _parentForPool;

    public static void InitParent(Transform parent)
    {
        _parentForPool = parent;
    }

    public static void PutObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(_parentForPool);
        gameObject.SetActive(false);

        if (_pool.ContainsKey(gameObject.name))
        {
            _pool[gameObject.name].AddLast(gameObject);
        }
        else
        {
            _pool[gameObject.name] = new LinkedList<GameObject>();
            _pool[gameObject.name].AddLast(gameObject);
        }
    }

    public static GameObject GetObject(GameObject gameObject)
    {
        GameObject result = null;
        if (_pool.ContainsKey(gameObject.name))
        {
            if (_pool[gameObject.name].Count > 0)
            {
                result = _pool[gameObject.name].Last.Value;
                _pool[gameObject.name].RemoveLast();
                result.transform.SetParent(null);
                return result;
            }
            else
            {
                result = GameObject.Instantiate(gameObject);
                result.name = gameObject.name;
                return result;
            }
        }
        else
        {
            _pool[gameObject.name] = new LinkedList<GameObject>();
            result = GameObject.Instantiate(gameObject);
            result.name = gameObject.name;
            return result;
        }
    }

    public static void PreparePoolObject(GameObject gameObject)
    {
        var instanceObject = GameObject.Instantiate(gameObject);
        instanceObject.name = gameObject.name;
        PutObject(instanceObject);
    }
}
