using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNoText : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private GameObject ShowText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _text.gameObject.SetActive(false);
            ShowText.SetActive(false);
            gameObject.SetActive(false);
        }
        
    }
}
