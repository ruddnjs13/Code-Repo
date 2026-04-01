using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.LowLevel;

public class Health : MonoBehaviour
{
    public UnityEvent OnhitEvenet;
    public UnityEvent OnDeadEvenet;
    private DamageText damageText;

    private Color _damageColor;

    [SerializeField] private int _maxHealth = 150;

    public int _currentHealth { get; private set; }
    private Agent _owner;

    private void Awake()
    {
        damageText = FindObjectOfType<DamageText>();

    }

    private void Update()
    {
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    private void Start()
    {
        ResetHealth();
        if (gameObject.CompareTag("Enemy"))
        {
            _damageColor = Color.white;
        }
        else
        {
            _damageColor = Color.red;
        }
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount) //, float KnokbackPower
    {
        _currentHealth -= amount;
        DamageText damageText = PoolManager.Instance.Pop("DamageText") as DamageText;
        damageText.PlayDamageText(amount, transform.position, _damageColor);
        OnhitEvenet?.Invoke();

        if (_currentHealth <= 0)
        {
            OnDeadEvenet?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            TakeDamage(5);
        }
    }

    public void PlayerHpSet(int value)
    {
        _currentHealth += value;
    }
}
