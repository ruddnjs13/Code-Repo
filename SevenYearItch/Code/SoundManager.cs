using System;
using System.Security.Authentication.ExtendedProtection;
using DewmoLib.ObjectPool.RunTime;
using Scripts.Core.Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00Work.LKW.Code
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolItemSO soundPlayer;

        private SoundPlayer bgmPlayer;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySFX(SoundSO sound, Vector3 pos)
        {
            SoundPlayer player = poolManager.Pop(soundPlayer) as SoundPlayer;
            player.PlaySound(sound, transform.position);
        }

    }
}