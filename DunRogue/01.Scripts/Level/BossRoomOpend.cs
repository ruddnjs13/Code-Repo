using DG.Tweening;
using UnityEngine;

public class BossRoomOpend : MonoBehaviour
{

    public void OpenBossRoom()
    {
        transform.Find("collider").gameObject.SetActive(false);
        transform.DOMoveY(transform.position.y - 2.6f, 2);
    }
}
