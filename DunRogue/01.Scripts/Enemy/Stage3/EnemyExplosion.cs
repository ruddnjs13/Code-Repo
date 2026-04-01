using UnityEngine;

public class EnemyExplosion : MonoBehaviour, IPoolable
{
    private Animator _animator;
    private DamageCaster damageCaster;

    private int _onExplosionHash = Animator.StringToHash("OnEnemyExplosion");
    private int _endExplosionHash = Animator.StringToHash("EndExplosion");

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    private void OnEnable()
    {
        PlayExplosion();
    }

    public void PlayExplosion()
    {
        _animator.SetTrigger(_onExplosionHash);
    }

    public void EndTrigger()
    {
        _animator.SetTrigger(_endExplosionHash);
        PoolManager.Instance.Push(this);
    }

    public void BombEnemyDamage()
    {
        if (Physics2D.OverlapCircle(transform.position, 1.5f, 7))
        {
            damageCaster.CastDamage(30);
        }
    }

    public void ResetItem()
    {
    }
}
