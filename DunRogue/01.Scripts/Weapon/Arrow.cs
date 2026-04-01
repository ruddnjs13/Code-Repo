using UnityEngine;

public class Arrow : MonoBehaviour, IPoolable
{
    private Rigidbody2D _rbCompo;
    private Weapon _weapon;
    private Vector2 _arrowDir;
    [SerializeField] private float _arrowSpeed = 10f;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    public void ResetItem()
    {
    }

    private void OnEnable()
    {
        _arrowDir = _weapon.transform.right;
    }

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
        _weapon = FindObjectOfType<Weapon>();
    }

    private void Update()
    {
        _rbCompo.velocity = _arrowDir * _arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") && Player.IsCombat)
        {
            Debug.Log("Attack");
            collision.GetComponent<Health>().TakeDamage(Player._weaponDamage);
            PoolManager.Instance.Push(this);
        }

        else if (collision.CompareTag("Wall"))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
