using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger : MonoBehaviour
{
    
    public UnityEvent OnEnter;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnEnter.Invoke();
            gameObject.SetActive(false);
        }
    }
}
