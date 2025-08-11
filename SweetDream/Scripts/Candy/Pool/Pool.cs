using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Stack<IPoolable> _pools;
    
    private GameObject _prefab;
    private Transform _parentTrm;
    private IPoolable _poolable;

    public Pool(IPoolable poolable, Transform parentTrm, int count)
    {
        _pools = new Stack<IPoolable>();
        _poolable = poolable;
        _parentTrm = parentTrm;
        _prefab = poolable.PoolObject;

        for (int i = 0; i < count; i++)
        {
            GameObject gameObj = GameObject.Instantiate(_prefab, _parentTrm);
            gameObj.SetActive(false);
            gameObj.name = _prefab.name;
            IPoolable item = gameObj.GetComponent<IPoolable>();
            if (item == null)
            {
                Debug.LogError($"not found IPoolable {_prefab.name}");
            }
            _pools.Push(item);
        }
    }

    public IPoolable Pop()
    {
        IPoolable item = null;
        
        if (_pools.Count == 0)
        {
            GameObject gameObj = GameObject.Instantiate(_prefab, _parentTrm);
            gameObj.name = item.PoolName;
            item = gameObj.GetComponent<IPoolable>();
        }
        else
        {
            item = _pools.Pop();
            item.PoolObject.SetActive(true);
        }
        
        return item;
    }

    public void Push(IPoolable item)
    {
        item.PoolObject.SetActive(false);
        _pools.Push(item);
    }
}
