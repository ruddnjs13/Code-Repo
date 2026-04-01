using UnityEngine;

public class GetWater : MonoBehaviour
{
    [SerializeField] private PlayerItemSo _playerItemSO;
    [SerializeField] private Sprite _waterPotion;
    private bool _playerIn = false;

    private void Update()
    {
        if (_playerIn && Input.GetKeyDown(KeyCode.E) && _playerItemSO.haveEmptyPotion)
        {
            _playerItemSO.haveWaterPotion = true;
            _playerItemSO.haveEmptyPotion = false;
            RewardManager.Instance.SetReward(_waterPotion, "²ËÂù¹°Åë");
            RewardManager.Instance.EnabledRewardPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIn = true;
        }
    }
}
