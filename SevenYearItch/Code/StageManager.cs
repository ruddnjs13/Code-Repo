using _00Work.LKW.Code;
using _00Work.LKW.Code.ETC;
using _00Work.LKW.Code.Events;
using DewmoLib.Utiles;
using Scripts.Core.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public static StageManager Instance => _instance;
    [SerializeField] private EventChannelSO stageChannel;
    [SerializeField] private SoundSO goalSound;

    [SerializeField] private StageListSO stageList;

    public int StageIndex { get; private set; } = 0;

    public int heartCount = 0;
    public int goalCount = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    private void OnEnable()
    {
        stageChannel.AddListener<GetHeartEvent>(HandleGetHeart);
        stageChannel.AddListener<PlayerGoalEvent>(HandlePlayerGoal);
    }


    private void OnDestroy()
    {
        stageChannel.RemoveListener<GetHeartEvent>(HandleGetHeart);
        stageChannel.RemoveListener<PlayerGoalEvent>(HandlePlayerGoal);
    }

    private void HandlePlayerGoal(PlayerGoalEvent evt)
    {
        goalCount++;
        if (goalCount >= 2)
        {
            SoundManager.Instance.PlaySFX(goalSound, transform.position);
            goalCount = 0;
            ChangeScene();
        }
    }

    private void HandleGetHeart(GetHeartEvent evt)
    {
        heartCount++;
        if (heartCount >= stageList[StageIndex].necessaryHeart)
        {
            stageChannel.InvokeEvent(StageEvent.EnableGoalEvent);
        }
    }

    private void ChangeScene()
    {
        heartCount = 0;
        if (++StageIndex > stageList.stages.Count - 1) return;
        SceneManager.LoadScene(stageList[StageIndex].stageName);
    }
}
