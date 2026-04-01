using UnityEngine;

public abstract class Enemy : Agent
{
    public float detectRadius;
    public float attackRadius;
    public float attackCooltime;
    public Vector2 boxSize;


    public bool CanSummon = false;
    [HideInInspector] public float lastAttackTime;

    public bool isPlayerFind = false;

    public bool isAttack;

    public DamageCaster damageCaster {  get; private set; }

    public bool canStateChangeable = true;


    public ContactFilter2D contactFilter;
    private Collider2D[] colliders;

    [HideInInspector] public Transform targetTrm = null;
    private int attackDamage = 10;

    protected override void Awake()
    {
        base.Awake();
        colliders = new Collider2D[1];
        damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    public Collider2D detectPlayer()
    {
        int value = Physics2D.OverlapCircle(transform.position, detectRadius, contactFilter, colliders);
        return value > 0 ? colliders[0] : null;
    }  
    public bool AttackZoneCheck()
    {
         return Physics2D.OverlapBox(transform.position, boxSize, 0, LayerMask.GetMask("Player"));
    }

    public abstract void AnimationEndTrigger();

    public virtual void Attack()
    {
        damageCaster.CastDamage(attackDamage);
    }

   

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

#endif

}
