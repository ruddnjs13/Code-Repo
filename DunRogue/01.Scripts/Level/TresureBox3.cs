using UnityEngine;

public class TresureBox3 : MonoBehaviour
{
    private bool _playerIn = false;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _openBox;
    [SerializeField] private PlayerItemSo _playerItemSo;
    [SerializeField] private Sprite _bossRoomKey2;

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
                RewardManager.Instance.SetReward(_bossRoomKey2, "데몬토템");
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
