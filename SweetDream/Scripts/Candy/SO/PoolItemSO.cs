using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/PoolItem")]
public class PoolItemSO : ScriptableObject
{
    public string poolName;
    public GameObject prefab;
    public int count;

    private void OnValidate()
    {
        if (prefab != null)
        {
            IPoolable poolable = prefab.GetComponent<IPoolable>();
            if (poolable == null)
            {
                Debug.LogError($"This is not Pool {prefab.name}");
                return;
            }

            poolName = poolable.PoolName;
        }
    }
}
