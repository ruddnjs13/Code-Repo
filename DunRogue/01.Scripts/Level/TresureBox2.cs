using UnityEngine;

public class TresureBox2 : MonoBehaviour
{
    private bool _playerIn = false;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _openBox;
    [SerializeField] private PlayerItemSo _playerItemSo;
    [SerializeField] private Sprite _bossRoomKey1;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_playerIn)
        {
            if (Input.GetKeyDown(KeyCode.E) && !_playerItemSo.haveBossRoomKey1)
            {
                _spriteRenderer.sprite = _openBox;
                _playerItemSo.haveBossRoomKey1 = true;
                RewardManager.Instance.SetReward(_bossRoomKey1, "언데드토템");
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
