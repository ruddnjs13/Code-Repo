using System.Collections;
using TMPro;
using UnityEngine;

public class Dino : MonoBehaviour
{
    [SerializeField] private CharacterDataSO _dinoSo;
    [SerializeField] private GameObject _dinoDialogUI;
    [SerializeField] private float _checkRadius;
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private TextMeshProUGUI _playerText;
    [SerializeField] private PlayerItemSo _playerItemSo;
    [SerializeField] private GameObject _dinoDutorial;
    private string _dinoDialog1 = "목이 마르다...";
    private string _dinoDialog2 = "덕분에 살았어";
    private string _dinoDialog3 = "너의 모험에 내가 함께 할께";
    private string _dinoDialog4 = "가지마";
    private string _playerDialog1 = "물병이 없다...";
    private string _playerDialog2 = "어디선가 얻을 수 있을지도?";
    public LayerMask playerLayer;
    private bool _playerFind = false;
    private bool _dinoDutorialFinish;
    private bool isChoose = false;

    Coroutine coroutine;

    private void Update()
    {
        if (_playerFind)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerFind = false;
                GameManager.Instance.StopPlayer();
                _dinoDialogUI.SetActive(true);
                if (_textMeshPro.text == "")
                    coroutine = StartCoroutine(ShowText(_dinoDialog1));
            }
        }
        else if (!_playerFind && coroutine == null)
        {
            _textMeshPro.text = "";
        }
        if (Input.anyKeyDown && _dinoDutorialFinish)
        {
            _dinoDutorial.SetActive(false);
            GameManager.Instance.MovePlayer();
            gameObject.SetActive(false);
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
        if (!_playerItemSo.haveWaterPotion && !isChoose)
        {
            isChoose = true;
            StartCoroutine(NoWaterPotion());
            return;
        }
        else if (_playerItemSo.haveWaterPotion)
        {
            StopAllCoroutines();
            _dinoSo.available = true;
            _dinoDialogUI.SetActive(false);
            StartCoroutine(HelpDino());
        }
    }

    private IEnumerator NoWaterPotion()
    {
        for (int i = 0; i < _playerDialog1.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _playerText.text += _playerDialog1[i];
        }
        yield return new WaitForSeconds(0.8f);
        _playerText.text = "";
        for (int i = 0; i < _playerDialog2.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _playerText.text += _playerDialog2[i];
        }
        yield return new WaitForSeconds(0.8f);
        _playerText.text = "";
        _textMeshPro.text = "";
        isChoose = false;
    }

    private IEnumerator HelpDino()
    {

        _textMeshPro.text = "";
        for (int i = 0; i < _dinoDialog2.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _dinoDialog2[i];
        }
        yield return new WaitForSeconds(0.8f);
        _textMeshPro.text = "";
        for (int i = 0; i < _dinoDialog3.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _dinoDialog3[i];
        }
        yield return new WaitForSeconds(0.8f);
        _dinoDutorial.SetActive(true);
        yield return new WaitForSeconds(2f);
        _textMeshPro.text = "";
        _dinoDutorialFinish = true;
    }

    public void NoButton()
    {
        StopAllCoroutines();
        _dinoDialogUI.SetActive(false);
        StartCoroutine(DontHelpDino());
    }

    private IEnumerator DontHelpDino()
    {
        _textMeshPro.text = "";
        for (int i = 0; i < _dinoDialog4.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _dinoDialog4[i];
        }
        yield return new WaitForSeconds(0.8f);
        _textMeshPro.text = "";
        _playerFind = false;
        GameManager.Instance.MovePlayer();

    }
}
