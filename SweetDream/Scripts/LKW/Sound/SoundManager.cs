using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum BGMEnum
{
    BGMTitle,
    BGMMap1,
    BGMMap2,
    BGMMap3
}

public enum SFXEnum
{
    LightShoot,
    LightReflect,
    MapRotate,
    Save,
    Lever,
    Butten,
    RotateArrow,
    Smahser,
    Landing,
    Move,
    Hit

}

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private BgmListData bgmListData;
    [SerializeField] private SfxListData sfxListData;

    [SerializeField] private SoundSettingSaveLoad saveLoad;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] protected Slider sfxSlider;
    
    AudioSource bgmPlayer;
    AudioSource[] sfxPlayers;
    

    public int channels = 16;
    int channelIndex;
    
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private BGMEnum PlayingBGM;

    private Dictionary<BGMEnum,AudioClip> _bgmDic = new Dictionary<BGMEnum,AudioClip>();
    private Dictionary<SFXEnum,AudioClip> _sfxDic = new Dictionary<SFXEnum,AudioClip>();

    private void Start()
    {
        Init();
        PlayBgm(PlayingBGM);
    }
    private void Init()
    {
        foreach (BgmSO item in bgmListData.bgmList)
        {
            _bgmDic.Add(item.bgmType,item.clip);
        }
        foreach (SfxSO item in sfxListData.sfxList)
        {
            _sfxDic.Add(item.sfxType,item.clip);
        }
        
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.GetComponent<AudioSource>().outputAudioMixerGroup = _mixer.FindMatchingGroups("BGM")[0];
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        //bgmPlayer.clip = bgmLis

        // 효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].outputAudioMixerGroup = _mixer.FindMatchingGroups("SFX")[0];
            sfxPlayers[i].playOnAwake = false;
        }
        
        saveLoad.LoadSoundDataFromJson();
        _mixer.SetFloat("MASTER", Mathf.Log10(saveLoad.SoundData.MasterSoundScale) * 20);
        _mixer.SetFloat("SFX", Mathf.Log10(saveLoad.SoundData.SFXSoundScale) * 20);
        _mixer.SetFloat("BGM", Mathf.Log10(saveLoad.SoundData.BGMSoundScale) * 20);
        masterSlider.value = saveLoad.SoundData.MasterSoundScale;
        bgmSlider.value = saveLoad.SoundData.BGMSoundScale;
        sfxSlider.value = saveLoad.SoundData.SFXSoundScale;

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
            sfxPlayers[loopIndex].clip = _sfxDic[sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void BgmStop()
    {
        bgmPlayer.Stop();
    }
    public void PlayBgm(BGMEnum bgm)
    {
        bgmPlayer.clip = _bgmDic[bgm];
        bgmPlayer.Play();
    }

    public void SetBgmVolume(float value)
    {
        _mixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        saveLoad.SoundData.BGMSoundScale = value;
        saveLoad.SaveSoundDataToJson();
        
    }
    public void SetSfxVolume(float value)
    {
        _mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        saveLoad.SoundData.SFXSoundScale = value;
        saveLoad.SaveSoundDataToJson();
    }
    public void SetMasterVolume(float value)
    {
        _mixer.SetFloat("MASTER", Mathf.Log10(value) * 20);
        saveLoad.SoundData.MasterSoundScale = value;
        saveLoad.SaveSoundDataToJson();
    }
}
