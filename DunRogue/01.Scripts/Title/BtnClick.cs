using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum BtnEnum
{
    Start,
    Exit,
    Quit,
    Setting
}

public class BtnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BtnEnum currentBtn;
    public Transform btnScale;
    private Vector3 defaultScale;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        defaultScale = btnScale.localScale;
    }

    public void OnClick()
    {
        switch (currentBtn)
        {
            case BtnEnum.Start:
                SceneManager.LoadScene(1);
                break;
            case BtnEnum.Exit:
                Application.Quit();
                Debug.Log("Exit");
                break;
            case BtnEnum.Setting:
                uiManager.SetSettingPanel();
                break;
            case BtnEnum.Quit:
                uiManager.Quit();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale;
    }
}
