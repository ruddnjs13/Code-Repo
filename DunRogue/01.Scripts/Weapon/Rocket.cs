using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, IPoolable
{
    [SerializeField] private float RocketSpeed = 12f;
    private Rigidbody2D _rbCompo;

 

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    public void ResetItem()
    {
    }

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rbCompo.velocity = transform.right * RocketSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Enemy"))
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        Explosion explosion =  PoolManager.Instance.Pop("Explosion") as Explosion;
        explosion.transform.position = transform.position;
        PoolManager.Instance.Push(this);
    }

  
}
