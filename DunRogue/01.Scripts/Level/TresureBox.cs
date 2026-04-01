using UnityEngine;

public class TresureBox : MonoBehaviour
{
    private bool _playerIn = false;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _openBox;
    [SerializeField] private PlayerItemSo _playerItemSo;
    [SerializeField] private Sprite _emptyPotion;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_playerIn)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_playerItemSo.haveEmptyPotion && !_playerItemSo.haveWaterPotion)
            {
                _spriteRenderer.sprite = _openBox;
                _playerItemSo.haveEmptyPotion = true;
                RewardManager.Instance.SetReward(_emptyPotion, "ºóÆ÷¼Çº´");
                RewardManager.Instance.EnabledRewardPanel();
                GameManager.Instance.StopPlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIn = false;
        }
    }
}
