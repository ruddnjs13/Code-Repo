using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    private bool _playerIn = false;
    private string dialog1 = "3개의 징표를 바치면 최후의 길이 열린다";

    private void Update()
    {
        if (_playerIn && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.StopPlayer();
            _playerIn = false;
            StartCoroutine(Dialog());
        }
    }

    private IEnumerator Dialog()
    {
        for (int i = 0; i < dialog1.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _text.text += dialog1[i].ToString();
        }
        yield return new WaitForSeconds(0.8f);
        _text.text = "";
        GameManager.Instance.MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIn = true;
        }
    }
}
