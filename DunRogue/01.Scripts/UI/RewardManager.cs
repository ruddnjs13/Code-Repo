using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoSingleton<RewardManager>
{
    [SerializeField] private GameObject _rewardPanel;
    [SerializeField] private Image _rewardItemImage;
    [SerializeField] private TextMeshProUGUI _rewardText;
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetReward(Sprite sprite, string str)
    {
        _rewardItemImage.sprite = sprite;
        _rewardText.text = str + "을 얻었다!!!";
    }
    public void EnabledRewardPanel()
    {
        _rewardPanel.SetActive(true);
        GameManager.Instance.StopPlayer();
    }
    public void DisabledRewardPanel()
    {
        _rewardPanel.SetActive(false);
        GameManager.Instance.MovePlayer();
    }

    internal void SetReward(object hamer, string v)
    {
        throw new NotImplementedException();
    }
}
