using UnityEngine;

public class MagicAttack : MonoBehaviour, IPoolable
{
    private Animator _animator;
    private DamageCaster _damageCaster;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }


    public void AttackTrigger()
    {
        _damageCaster.CastDamage(20);
    }
    public void EndTrigger()
    {
        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {

    }


}
