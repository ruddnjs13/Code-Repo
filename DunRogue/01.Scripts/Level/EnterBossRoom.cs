using UnityEngine;

public class EnterBossRoom : MonoBehaviour
{
    [SerializeField] private GameObject BossUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.Playbgm(BGMEnum.BossRoomBgm);
            BossUI.SetActive(true);
        }
    }
}
