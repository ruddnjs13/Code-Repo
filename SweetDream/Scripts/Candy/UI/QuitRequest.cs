using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitRequest : MonoBehaviour
{
    [SerializeField] private Image quitPanel;

    public void Quit()
    {
        quitPanel.gameObject.SetActive(true);
    }

    public void No()
    {
        quitPanel.gameObject.SetActive(false);
    }
}
