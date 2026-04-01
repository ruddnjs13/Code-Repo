using UnityEngine;

public class ToNextStage : MonoBehaviour
{
    private bool isEnter;
    private GameObject Player;
    private Health _health;

    private void Update()
    {
        ToNextStages();
    }

    private void ToNextStages()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.F))
        {
            Player.transform.position = GameManager.Instance.stagePos[GameManager.stageIndex++].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("상호작용가능");
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
