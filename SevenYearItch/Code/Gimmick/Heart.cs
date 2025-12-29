using _00Work.LKW.Code.Events;
using DewmoLib.Utiles;
using DG.Tweening;
using Scripts.Core.Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00Work.LKW.Code.Gimmick
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private EventChannelSO stageChannel;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider2D collider;
        [SerializeField] private SoundSO getSound;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & whatIsPlayer) != 0)
            {
                collider.enabled = false;
                animator.SetTrigger("DISAPPEAR");
                SoundManager.Instance.PlaySFX(getSound,transform.position);
            }
        }

        public void AnimationEnd()
        {
            Destroy(gameObject);
            stageChannel.InvokeEvent(StageEvent.GetHeartEvent);
        }
    }
}