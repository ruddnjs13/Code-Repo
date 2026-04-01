using System.Collections;
using TMPro;
using UnityEngine;

public class YelloBaba : MonoBehaviour
{
    [SerializeField] private GameObject _yBabaDialogUI;
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private PlayerItemSo _playerItemSo;
    private string _yBabaDialog1 = "간단한 문제를 맟추면 주운 이 망치를 주지";
    private string _yBabaDialog2 = "아니 정말 똑똑하군 천재야";
    private string _yBabaDialog3 = "정말 몽춍몽춍하군........";
    private bool _playerFind = false;

    Coroutine coroutine;
    [SerializeField] private Sprite _hamer;

    private void Update()
    {
        if (_playerFind)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerFind = false;
                GameManager.Instance.StopPlayer();
                _yBabaDialogUI.SetActive(true);
                if (_textMeshPro.text == "")
                    coroutine = StartCoroutine(ShowText(_yBabaDialog1));
            }
        }
        else if (!_playerFind && coroutine == null)
        {
            _textMeshPro.text = "";
        }
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


    private IEnumerator ShowText(string dialog)
    {
        for (int i = 0; i < dialog.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += dialog[i];
        }
    }

    public void YesButton()
    {
        StopAllCoroutines();
        _yBabaDialogUI.SetActive(false);
        StartCoroutine(Answer1());
    }

    private IEnumerator Answer1()
    {

        _textMeshPro.text = "";
        for (int i = 0; i < _yBabaDialog2.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _yBabaDialog2[i];
        }
        yield return new WaitForSeconds(0.8f);
        _textMeshPro.text = "";
        _playerItemSo.haveHammer = true;
        RewardManager.Instance.SetReward(_hamer, "망치");
        RewardManager.Instance.EnabledRewardPanel();


    }

    public void NoButton()
    {
        StopAllCoroutines();
        _yBabaDialogUI.SetActive(false);
        StartCoroutine(Answer2());
    }

    private IEnumerator Answer2()
    {
        _textMeshPro.text = "";
        for (int i = 0; i < _yBabaDialog3.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _yBabaDialog3[i];
        }
        yield return new WaitForSeconds(0.8f);
        _textMeshPro.text = "";
        _playerFind = false;
        GameManager.Instance.MovePlayer();

    }
}
