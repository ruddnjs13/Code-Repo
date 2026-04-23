using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum BGM
{
    BGMTitle,
    BGMMap1,
    BGMMap2,
    BGMMap3
}

public enum SFX
{
    LightShoot,
    LightReflect,
    MapRotate,
    Save,
    Lever,
    Button,
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
    
    private AudioSource _bgmPlayer;
    private AudioSource[] _sfxPlayers;
    

    public int channels = 16;
    private int _channelIndex;
    
     [SerializeField] private AudioMixer mixer;
     [SerializeField] private BGM playingBGM;

    private readonly Dictionary<BGM,AudioClip> _bgmDic = new Dictionary<BGM,AudioClip>();
    private readonly Dictionary<SFX,AudioClip> _sfxDic = new Dictionary<SFX,AudioClip>();

    private void Start()
    {
        Init();
        PlayBgm(playingBGM);
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
        _bgmPlayer = bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.GetComponent<AudioSource>().outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        _bgmPlayer.playOnAwake = false;
        _bgmPlayer.loop = true;
        //bgmPlayer.clip = bgmLis

        // 효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            _sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[i].outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
            _sfxPlayers[i].playOnAwake = false;
        }
        
        saveLoad.LoadSoundDataFromJson();
        mixer.SetFloat("MASTER", Mathf.Log10(saveLoad.SoundData.MasterSoundScale) * 20);
        mixer.SetFloat("SFX", Mathf.Log10(saveLoad.SoundData.SFXSoundScale) * 20);
        mixer.SetFloat("BGM", Mathf.Log10(saveLoad.SoundData.BGMSoundScale) * 20);
        masterSlider.value = saveLoad.SoundData.MasterSoundScale;
        bgmSlider.value = saveLoad.SoundData.BGMSoundScale;
        sfxSlider.value = saveLoad.SoundData.SFXSoundScale;

    }
    
    public void PlaySfx(SFX sfx)
    {
        for (int i = 0; i < _sfxPlayers.Length; i++)
        {
            int loopIndex = (i + _channelIndex) % _sfxPlayers.Length;
            if (_sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            _channelIndex = loopIndex;
            _sfxPlayers[loopIndex].clip = _sfxDic[sfx];
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void BgmStop()
    {
        _bgmPlayer.Stop();
    }
    public void PlayBgm(BGM bgm)
    {
        _bgmPlayer.clip = _bgmDic[bgm];
        _bgmPlayer.Play();
    }

    public void SetBgmVolume(float value)
    {
        mixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        saveLoad.SoundData.BGMSoundScale = value;
        saveLoad.SaveSoundDataToJson();
        
    }
    public void SetSfxVolume(float value)
    {
        mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        saveLoad.SoundData.SFXSoundScale = value;
        saveLoad.SaveSoundDataToJson();
    }
    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MASTER", Mathf.Log10(value) * 20);
        saveLoad.SoundData.MasterSoundScale = value;
        saveLoad.SaveSoundDataToJson();
    }
}
