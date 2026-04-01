using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public ContactFilter2D filter;
    public float damageRadius;
    public int detectCount = 1;

    private Collider2D[] _colliders;

    private void Awake()
    {
        _colliders = new Collider2D[detectCount];
    }

    public bool CastDamage(int damage) // float knokbackPower
    {
        int cnt = Physics2D.OverlapCircle(transform.position, damageRadius, filter, _colliders);

        for (int i = 0; i < cnt; i++)
        {
            if (_colliders[i].TryGetComponent(out Health health))
            {
                Vector2 dir = _colliders[i].transform.position - transform.position;

                //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, dir.magnitude, filter.layerMask);

                health.TakeDamage(damage);  //  float knokbackPower
            }
        }
        return cnt > 0;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
        Gizmos.color = Color.white;
    }

#endif
}
