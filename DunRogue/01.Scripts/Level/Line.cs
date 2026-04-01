using DG.Tweening;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;

    public bool isFirst = true;

    private void OnDisable()
    {
        wall1.transform.DOKill();
        wall2.transform.DOKill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isFirst)
        {

            wall1.SetActive(true);
            wall2.SetActive(true);
            UpWall(wall1.transform);
            UpWall(wall2.transform);
            Player.IsCombat = true;
            isFirst = false;
        }
    }

    public void UpWall(Transform trm)
    {
        trm.DOMoveY(trm.transform.position.y + 2.6f, 0.5f);
    }



}
