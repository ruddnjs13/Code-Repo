using DewmoLib.Utiles;
using DG.Tweening;
using UnityEngine;

namespace _00Work.LKW.Code.Gimmick
{
    public class AppearWall : MonoBehaviour
    {
        [SerializeField] private EventChannelSO stageChannel;
        [SerializeField] private LayerMask whatIsPlayer, whatIsTile;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D collider;
        [SerializeField] private GameObject boarder;

        bool isTriggered = false;

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("asss");
            if (((1 << collision.gameObject.layer) & whatIsPlayer) > 0 && !isTriggered)
            {
                Debug.Log("ASD");
                isTriggered = true;
                boarder.SetActive(false);
                gameObject.layer = 6;
                collider.isTrigger = false;
                collider.size = Vector2.one;
                DOVirtual.DelayedCall(0.2f, () => spriteRenderer.enabled = true);
            }
        }
    }
}