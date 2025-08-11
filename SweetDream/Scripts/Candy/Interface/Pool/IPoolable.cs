using UnityEngine;

public interface IPoolable
{
    public string PoolName { get; }
    public GameObject PoolObject { get; }

    public void ResetItem();
}
