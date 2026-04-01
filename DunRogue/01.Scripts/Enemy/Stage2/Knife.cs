using UnityEngine;

public class Knife : MonoBehaviour
{
    private Oak _enemy;
    private DamageCaster _damageCaster;


    private void Awake()
    {
        _enemy = GetComponentInParent<EnemyWeapon>().GetComponentInParent<Oak>();
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _enemy.isAttack)
        {
            _damageCaster.CastDamage(10);
            _enemy.isAttack = false;
        }
    }
}
