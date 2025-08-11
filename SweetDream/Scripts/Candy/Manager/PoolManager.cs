using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolListSO _poolList;
    
    public Dictionary<string, Pool> pools;

    private void Awake()
    {
        pools = new Dictionary<string, Pool>();

        foreach (var item in _poolList.pools)
        {
            CreatePool(item);
        }
    }

    private void CreatePool(PoolItemSO item)
    {
        IPoolable poolable = item.prefab.GetComponent<IPoolable>();

        if (poolable == null)
        {
            Debug.LogError($"This is not Pool {item.prefab.name}");
            return;
        }
        Debug.Log(poolable);
        Debug.Log(item);

        Pool pool = new Pool(poolable, transform, item.count);
        
        pools.Add(poolable.PoolName, pool);
    }

    public IPoolable Pop(string poolName)
    {
        if (pools.ContainsKey(poolName))
        {
            IPoolable item = pools[poolName].Pop();
            item.ResetItem();
            return item;
        }
        
        Debug.LogError($"Pool {poolName} not found");
        return null;
    }

    public void Push(IPoolable item)
    {
        if (pools.ContainsKey(item.PoolName))
        {
            pools[item.PoolName].Push(item);
            return;
        }
        
        Debug.LogError($"Pool {item.PoolName} not found");
    }
}
