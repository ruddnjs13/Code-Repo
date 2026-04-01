using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Artar : MonoBehaviour
{
    [SerializeField] private GameObject Totem;
    [SerializeField] private PlayerItemSo PlayerItemSo;
    private bool _playerFind = false;
    public static int totemCount = 3;
    public UnityEvent BossRoomOpenTrigger;

    private void Update()
    {
        if (!PlayerItemSo.haveBossRoomKey1 && !PlayerItemSo.haveBossRoomKey2 && !PlayerItemSo.haveBossRoomKey3) return;
        if (_playerFind && Input.GetKeyDown(KeyCode.E))
        {
            Totem.SetActive(true);
            totemCount--;
        }
        if (totemCount == 0)
        {
            StartCoroutine(OpenDelay());
            totemCount++;
        }
    }

    private IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(1.8f);
        BossRoomOpenTrigger?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerFind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerFind = false;
        }
    }
}
