using UnityEngine;

public class AnimationEndTrigger : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public void AnimatioEndTrigger()
    {
        _enemy.AnimationEndTrigger();
    }

    public void AniamtioAttackTrigger()
    {
        _enemy.Attack();
    }

    public void AttackTigger()
    {
        _enemy.isAttack = true;
    }
}
