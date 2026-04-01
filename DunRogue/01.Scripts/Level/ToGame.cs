using UnityEngine;

public class ToGame : MonoBehaviour
{
    private bool isEnter;
    private GameObject Player;

    private void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.F))
        {
            Player.transform.position = Vector3.zero;

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
