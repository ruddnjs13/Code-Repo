using UnityEngine;

public class EnemyArrow : MonoBehaviour, IPoolable
{
    private Rigidbody2D _rbCompo;
    [SerializeField] private float _arrowSpeed = 10f;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    public void ResetItem()
    {
    }

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rbCompo.velocity = transform.right * _arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(10);
            PoolManager.Instance.Push(this);
        }

        else if (collision.CompareTag("Wall"))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
