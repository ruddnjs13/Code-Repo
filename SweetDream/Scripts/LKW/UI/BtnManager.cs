using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum BtnType
{
    None,
    Start,
    Setting,
    Exit,
    Video,
    Audio,
    KeySetting,
    Quit,
    OptionQuit
}

public class BtnManager : MonoSingleton<BtnManager>
{
    [SerializeField] private GameObject _settingUI;
    [SerializeField] private GameObject _titleUI;
    [SerializeField] private GameObject _videoPanel;
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private GameObject _KeyboardPanel;
    [SerializeField] private GameObject _optionPanel;
    BtnType _btnType = BtnType.None;
    
    private FeedbackPlayer _feedbackPlayer;

    private void Awake()
    {
        _feedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
    }

    private void BtnClick()
    {
        SoundManager.Instance.PlaySfx(SFXEnum.Butten);
        switch (_btnType)
        {
            case BtnType.Start: 
                SceneManager.LoadScene(6);
                break;
            case BtnType.Setting:
                _titleUI.SetActive(false);
                _settingUI.SetActive(true);
                break;
            case BtnType.Exit:
                Application.Quit();
                break;
            case BtnType.Video:
                _videoPanel.SetActive(true);
                _optionPanel.SetActive(false);
                break;
            case BtnType.Audio:
                _audioPanel.SetActive(true);
                _optionPanel.SetActive(false);
                break;
            case BtnType.KeySetting:
                _KeyboardPanel.SetActive(true);
                _optionPanel.SetActive(false);
                break;
            case BtnType.Quit:
                _titleUI.SetActive(true);
                _settingUI.SetActive(false);
                break;
            case BtnType.OptionQuit:
                _videoPanel.SetActive(false);
                _audioPanel.SetActive(false);
                _KeyboardPanel.SetActive(false);
                _optionPanel.SetActive(true);
                break;
        }
    }

    public void SetBtnAndClick(BtnType btnType)
    {
        _btnType = btnType;
        BtnClick();
    }
}
