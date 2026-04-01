using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BGMEnum
{
    stg1BGM,
    stg2BGM,
    stg3BGM,
    BossRoomBgm
}

public enum SFXEnum
{
    Reload,
    ShootGun,
    Rocket,
    Arrow,
    BowCharge
}

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private Slider _sfxSliider;
    [SerializeField] private Slider _bgmSliider;

    [Header("BGM")]
    public List<AudioClip> bgmList = new List<AudioClip>();
    public static float bgmVolume = 0.5f;
    AudioSource bgmPlayer;

    [Header("SFX")]
    public List<AudioClip> sfxList = new List<AudioClip>();
    public static float sfxVolume = 0.5f;
    AudioSource[] sfxPlayers;
    public int channels = 16;
    int channelIndex;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        bgmPlayer.volume = bgmVolume;
        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    private void Init()
    {
        // 배경음 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        _bgmSliider.value = bgmVolume;
        //bgmPlayer.clip = bgmLis

        // 효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        _sfxSliider.value = sfxVolume;
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlaySfx(SFXEnum sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxList[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void BgmStop()
    {
        bgmPlayer.Stop();
    }
    public void Playbgm(BGMEnum bgm)
    {
        bgmPlayer.clip = bgmList[(int)bgm];
        bgmPlayer.Play();
    }

    public void SetBgmVolum(float value)
    {
        bgmVolume = value;
    }
    public void SetSfxVolum(float value)
    {
        sfxVolume = value;
    }

}
