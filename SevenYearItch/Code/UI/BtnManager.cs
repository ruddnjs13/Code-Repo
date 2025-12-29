using _01.Code.UI;
using Scripts.Core.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00Work.LKW.Code.UI
{
    public class BtnManager : MonoBehaviour
    {
        public static BtnManager Instance;
        [SerializeField] private SoundSO buttonSound;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        [SerializeField] private string nextSceneName;
        [SerializeField] private SettingUI settingUI;

        public void CheckButton(BtnType btnType)
        {
            SoundManager.Instance.PlaySFX(buttonSound, transform.position);
            switch (btnType)
            {
                case BtnType.Start:
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case BtnType.Setting:
                    settingUI.Setup(true);
                    break;
                case BtnType.Exit:
                    Application.Quit();
                    break;
                case BtnType.Quit:
                    settingUI.Setup(false);
                    break;
            }
        }
        
    }
}