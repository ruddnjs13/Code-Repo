using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform Pos2;
    [SerializeField] private Transform Pos3;
    [SerializeField] private GameObject SettingPanel;

    public void SetSettingPanel()
    {
        SettingPanel.transform.DOMoveY(Pos2.position.y, 1.2f)
            .SetEase(Ease.InOutQuint);
    }
    public void Quit()
    {
        SettingPanel.transform.DOMoveY(Pos3.position.y, 1f)
            .SetEase(Ease.InOutQuint);
    }
}
