using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/PoolList")]
public class PoolListSO : ScriptableObject
{
    public List<PoolItemSO> pools;
}
