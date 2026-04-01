using System;
using System.Linq.Expressions;

public class BossSummon : EnemyState
{
    public BossSummon(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        EnemySummon();
    }

    private void EnemySummon()
    {
        int MonsterNum = UnityEngine.Random.Range(0, 3);
        switch(MonsterNum)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    Demon demon = PoolManager.Instance.Pop("Demon") as Demon;
                    demon.transform.SetPositionAndRotation(GameManager.Instance.enemySpawnPos[i].position, GameManager.Instance.enemySpawnPos[i].rotation);
                    _enemy.CanSummon = false;
                }
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    Goblin goblin = PoolManager.Instance.Pop("Goblin") as Goblin;
                    goblin.transform.SetPositionAndRotation(GameManager.Instance.enemySpawnPos[i].position, GameManager.Instance.enemySpawnPos[i].rotation);
                    _enemy.CanSummon = false;
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    Oak Ork = PoolManager.Instance.Pop("Oak") as Oak;
                    Ork.transform.SetPositionAndRotation(GameManager.Instance.enemySpawnPos[i].position, GameManager.Instance.enemySpawnPos[i].rotation);
                    _enemy.CanSummon = false;
                }
                break;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(BossEnum.BossChase);
        }
    }
}
