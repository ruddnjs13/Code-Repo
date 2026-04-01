using UnityEngine;

public class MagicBossMove : EnemyState
{
    public MagicBossMove(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Vector3 dir = _enemy.targetTrm.transform.position - _enemy.transform.position;
        _enemy.MovementCompo.SetMovement(dir.normalized);

        float distance = Vector2.Distance(_enemy.targetTrm.position, _enemy.transform.position);
        if (distance <= _enemy.attackRadius)
        {
            _enemy.MovementCompo.SetMovement(Vector2.zero);
            int attackNum = Random.Range(0, 3);
            if (_enemy.lastAttackTime + _enemy.attackCooltime > Time.time) return;
            switch (attackNum)
            {
                case 0:
                    _enemy.MovementCompo.SetMovement(Vector2.zero);
                    _stateMachine.ChangeState(MagicBossEnum.BossAttack1);
                    break;
                case 1:
                    _enemy.MovementCompo.SetMovement(Vector2.zero);

                    _stateMachine.ChangeState(MagicBossEnum.BossAttack2);
                    break;
                case 2:
                    _enemy.MovementCompo.SetMovement(Vector2.zero);
                    _stateMachine.ChangeState(MagicBossEnum.BossAttack3);
                    break;
            }
            return;
        }
    }
}
