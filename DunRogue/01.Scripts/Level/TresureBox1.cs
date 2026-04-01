using UnityEngine;

public class TresureBox1 : MonoBehaviour
{
    private bool _playerIn = false;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _openBox;
    [SerializeField] private PlayerItemSo _playerItemSo;
    [SerializeField] private Sprite _bossRoomKey3;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_playerIn)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_playerItemSo.haveBossRoomKey3)
            {
                _spriteRenderer.sprite = _openBox;
                _playerItemSo.haveBossRoomKey1 = true;
                RewardManager.Instance.SetReward(_bossRoomKey3, "오크토템");
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
