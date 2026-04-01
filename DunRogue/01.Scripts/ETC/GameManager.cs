using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public Transform[] enemySpawnPos;
    public Transform[] stagePos;
    public static int stageIndex = 0;
    private Player player;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private PlayerItemSo _plaeyrItemSo;
    [SerializeField] private CharacterDataSO _babarian;
    [SerializeField] private CharacterDataSO _dino;
    [SerializeField] private CharacterDataSO _archer;

    [SerializeField] private GameObject GameClearMenu;

    private string GameOver = "YOUDIE";
    private bool _panelIsMove = false;
    [SerializeField] private Texture2D _cursorIcon;

    [field: SerializeField] public GameObject Player { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.Playbgm(BGMEnum.stg1BGM);
        Cursor.SetCursor(_cursorIcon, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_panelIsMove)
        {
            if (!isOnSettingPanel)
            {
                SetSettingPanel();
            }
            else
            {
                Quit();
            }
        }
    }

    public void SetDieUI(Player player)
    {
        if (!player.isDead) return;
        _gameOverPanel.SetActive(true);
        StartCoroutine(ShowGameOver());
    }

    public void Restart()
    {
        Debug.Log("Click");
        _plaeyrItemSo.haveEmptyPotion = false;
        _plaeyrItemSo.haveWaterPotion = false;
        _plaeyrItemSo.haveHammer = false;
        _plaeyrItemSo.haveBossRoomKey1 = false;
        _plaeyrItemSo.haveBossRoomKey2 = false;
        _plaeyrItemSo.haveBossRoomKey3 = false;
        _babarian.available = false;
        _dino.available = false;
        _archer.available = false;

        stageIndex = 0;
        SceneManager.LoadScene(1);
    }

    private IEnumerator ShowGameOver()
    {
        for (int i = 0; i < GameOver.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            _gameOverText.text += GameOver[i] + " ";
        }
        Time.timeScale = 0;
    }

    public void StopPlayer()
    {
        player.MovementCompo.Stop();
    }
    public void MovePlayer()
    {
        player.MovementCompo.Move();
    }

    public void GameClear()
    {

        SoundManager.Instance.BgmStop();
        StopPlayer();
        GameClearMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void ToTitle()
    {
        _plaeyrItemSo.haveEmptyPotion = false;
        _plaeyrItemSo.haveWaterPotion = false;
        _plaeyrItemSo.haveHammer = false;
        _plaeyrItemSo.haveBossRoomKey1 = false;
        _plaeyrItemSo.haveBossRoomKey2 = false;
        _plaeyrItemSo.haveBossRoomKey3 = false;
        _babarian.available = false;
        _dino.available = false;
        _archer.available = false;
        stageIndex = 0;
        SceneManager.LoadScene(0);
    }

    public void StopBtn()
    {
        if (isOnSettingPanel) return;
        StopPlayer();
        SetSettingPanel();
    }


    [SerializeField] private Transform Pos2;
    [SerializeField] private Transform Pos3;
    [SerializeField] private GameObject SettingPanel;
    private bool isOnSettingPanel;

    public void SetSettingPanel()
    {
        _panelIsMove = true;
        isOnSettingPanel = true;
        SettingPanel.transform.DOMoveY(Pos2.position.y, 1.2f)
            .SetEase(Ease.InOutQuint)
            .OnComplete(() => { Time.timeScale = 0; _panelIsMove = false; });
    }
    public void Quit()
    {
        if (!isOnSettingPanel) return;
        Time.timeScale = 1;
        MovePlayer();
        _panelIsMove = true;
        isOnSettingPanel = false;
        SettingPanel.transform.DOMoveY(Pos3.position.y, 1f)
            .SetEase(Ease.InOutQuint)
            .OnComplete(() => _panelIsMove = false);
    }

}
