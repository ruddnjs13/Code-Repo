using System;
using _00Work.LKW.Code.Events;
using AKH.Scripts.Players;
using DewmoLib.Dependencies;
using DewmoLib.Utiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00Work.LKW.Code.Gimmick
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private EventChannelSO stageChannel;
        [SerializeField] private BoxCollider2D collier;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private LayerMask whatIsPlayer;
        [SerializeField] private GameObject boarder;
        [Inject] private SwapManager swapManager;

        private void OnEnable()
        {
            stageChannel.AddListener<EnableGoalEvent>(HandleEnableGoalEvent);
        }

        private void OnDestroy()
        {
            stageChannel.RemoveListener<EnableGoalEvent>(HandleEnableGoalEvent);
        }

        private void HandleEnableGoalEvent(EnableGoalEvent evt)
        {
            spriteRenderer.enabled = true;
            collier.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (((1 << collision.gameObject.layer) & whatIsPlayer) > 0)
            {
                stageChannel.InvokeEvent(StageEvent.PlayerGoalEvent);
                spriteRenderer.enabled = false;
                collier.enabled = false;
                boarder.SetActive(false);
                swapManager.FixCam();
                swapManager.canSwap = false;
                collision.gameObject.SetActive(false);
            }
        }
    }
}