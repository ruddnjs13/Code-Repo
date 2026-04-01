using UnityEngine;

public class BossChase : EnemyState
{
    public BossChase(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 dir = _enemy.targetTrm.transform.position - _enemy.transform.position;
        _enemy.MovementCompo.SetMovement(dir.normalized);

        float distance = Vector2.Distance(_enemy.targetTrm.position, _enemy.transform.position);

        if (_enemy.AttackZoneCheck() && _enemy.lastAttackTime + _enemy.attackCooltime < Time.time)
        {
            _enemy.MovementCompo.SetMovement(Vector2.zero);
            _stateMachine.ChangeState(BossEnum.BossAttack);
            return;
        }
        else if (distance > 6 && _enemy.lastAttackTime + _enemy.attackCooltime < Time.time && Random.Range(0, 3) < 1)
        {
            _enemy.MovementCompo.SetMovement(Vector2.zero);
            _stateMachine.ChangeState(BossEnum.BossSummon);
            return;
        }
        else if (_enemy.lastAttackTime + _enemy.attackCooltime < Time.time)
        {
            _enemy.MovementCompo.SetMovement(Vector2.zero);
            _stateMachine.ChangeState(BossEnum.BossSpell);
            return;
        }

    }
}
