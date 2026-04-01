using UnityEngine;

public class Explosion : MonoBehaviour, IPoolable
{
    private Animator _animator;
    private DamageCaster damageCaster;

    private int _onExplosionHash = Animator.StringToHash("OnExplosion");
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy") && Player.IsCombat)
    //    {
    //        Debug.Log("공격성공");
    //    }
    //}

    public void BombDamage()
    {
        if (Physics2D.OverlapCircle(transform.position, 1.5f, 7) && Player.IsCombat)
        {
            damageCaster.CastDamage(Player._weaponDamage);
        }
    }

    public void ResetItem()
    {
    }
}
