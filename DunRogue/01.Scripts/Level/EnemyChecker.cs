using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    [SerializeField] private GameObject wall1;
    [SerializeField] private GameObject wall2;
    [SerializeField] private GameObject _line;

    public int _enemyCount = 6;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyCount--;
            if (_enemyCount == 0 && wall1.activeInHierarchy && wall2.activeInHierarchy)
            {
                StartCoroutine(DelayDownWall());
            }
        }
    }

    public void DownWall(Transform trm)
    {
        trm.DOMoveY(trm.transform.position.y - 2.6f, 0.5f);
    }

    private IEnumerator DelayDownWall()
    {
        Player.IsCombat = false;
        DownWall(wall1.transform);
        DownWall(wall2.transform);
        yield return new WaitForSeconds(0.6f);
        wall1.SetActive(false);
        wall2.SetActive(false);
        _line.SetActive(false);
    }
}
