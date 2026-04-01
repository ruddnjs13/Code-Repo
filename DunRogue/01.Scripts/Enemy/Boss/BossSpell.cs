using UnityEngine;

public class BossSpell : EnemyState
{
    public BossSpell(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastAttackTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.isAttack)
        {
            MagicAttack magicAttack = PoolManager.Instance.Pop("BossMagicAttack") as MagicAttack;
            magicAttack.transform.SetPositionAndRotation(new Vector3(_enemy.targetTrm.position.x, _enemy.targetTrm.position.y + 2, _enemy.targetTrm.position.z), Quaternion.identity);
            _enemy.isAttack = false;
        }
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(BossEnum.BossChase);
        }
    }
}
