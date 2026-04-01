using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    private Rigidbody2D rbCompo;

    private DamageCaster damageCaster;

    [SerializeField] float _bulletSpeed = 10f;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    public void ResetItem()
    {
    }

    private void Awake()
    {
        rbCompo = GetComponent<Rigidbody2D>();
        damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    void Update()

    {
        rbCompo.velocity = transform.right * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Player.IsCombat)
        {
            damageCaster.CastDamage(Player._weaponDamage);
            PoolManager.Instance.Push(this);
        }

        else if (collision.CompareTag("Wall"))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
