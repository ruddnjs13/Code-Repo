using UnityEngine;

public class DemonExplosion : EnemyState
{
    public DemonExplosion(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.MovementCompo.SetMovement((_enemy.targetTrm.position - _enemy.transform.position).normalized);
        if (_endTriggerCalled)
        {
            EnemyExplosion enemyExplosion = PoolManager.Instance.Pop("EnemyExplosion") as EnemyExplosion;
            enemyExplosion.transform.SetPositionAndRotation(_enemy.transform.position, Quaternion.identity);
            _enemy.isAttack = true;
        }
    }
}
