using Core.Define;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Setting
{
    public class SettingView : MonoBehaviour, IView
    {
        [Header("<color=yellow>[ Setting UI ]</color>")]
        [SerializeField] private GameObject settingUI;
        [SerializeField] private TMP_Dropdown screenModeDropdown;
        [SerializeField] private Button closeButton;
        [SerializeField] private Slider masterVolumeSlider, effectVolumeSlider, musicVolumeSlider;

        private SettingViewModel _viewModel;

        private void Awake()
        {
            _viewModel = SettingViewModel.Instance;

            BindViewToViewModel();
            BindViewModelToView();
            
            SetDefaultSetting();
        }

        private void OnDestroy()
        {
            UnbindAll();
        }

        public void BindViewToViewModel()
        {
            screenModeDropdown.onValueChanged.AddListener(_viewModel.ApplyScreenMode);
            masterVolumeSlider.onValueChanged.AddListener(_viewModel.SetMasterVolume);
            musicVolumeSlider.onValueChanged.AddListener(_viewModel.SetMusicVolume);
            effectVolumeSlider.onValueChanged.AddListener(_viewModel.SetEffectVolume);
            closeButton.onClick.AddListener(_viewModel.Close);
        }

        public void BindViewModelToView()
        {
            _viewModel.IsOpen.Subscribe(SetActive);
            _viewModel.ScreenModeIndex.Subscribe(SetScreenMode);
            _viewModel.MasterVolume.Subscribe(SetMasterVolume);
            _viewModel.BGMVolume.Subscribe(SetMusicVolume);
            _viewModel.SFXVolume.Subscribe(SetEffectVolume);
        }

        public void UnbindAll()
        {
            screenModeDropdown.onValueChanged.RemoveListener(_viewModel.ApplyScreenMode);
            masterVolumeSlider.onValueChanged.RemoveListener(_viewModel.SetMasterVolume);
            musicVolumeSlider.onValueChanged.RemoveListener(_viewModel.SetMusicVolume);
            effectVolumeSlider.onValueChanged.RemoveListener(_viewModel.SetEffectVolume);
            closeButton.onClick.RemoveListener(_viewModel.Close);
            
            _viewModel.IsOpen.Unsubscribe(SetActive);
            _viewModel.ScreenModeIndex.Unsubscribe(SetScreenMode);
            _viewModel.MasterVolume.Unsubscribe(SetMasterVolume);
            _viewModel.BGMVolume.Unsubscribe(SetMusicVolume);
            _viewModel.SFXVolume.Unsubscribe(SetEffectVolume);
        }

        private void SetDefaultSetting()
        {
            _viewModel.ApplyScreenMode(PlayerPrefs.GetInt(ConstDefine.ScreenModePrefsKey));
            _viewModel.SetMasterVolume(PlayerPrefs.GetFloat(ConstDefine.MasterVolumePrefsKey));
            _viewModel.SetMusicVolume(PlayerPrefs.GetFloat(ConstDefine.MusicVolumePrefsKey));
            _viewModel.SetEffectVolume(PlayerPrefs.GetFloat(ConstDefine.EffectVolumePrefsKey));
        }

        #region Bind Method

        private void SetActive(bool isOpen) => settingUI?.SetActive(isOpen);

        private void SetScreenMode(int index) => screenModeDropdown.SetValueWithoutNotify(index);
        
        private void SetMasterVolume(float volume) => masterVolumeSlider.SetValueWithoutNotify(volume);
        
        private void SetMusicVolume(float volume) => musicVolumeSlider.SetValueWithoutNotify(volume);
        
        private void SetEffectVolume(float volume) => effectVolumeSlider.SetValueWithoutNotify(volume);

        #endregion
    }
}