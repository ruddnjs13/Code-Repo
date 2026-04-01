using System.Collections;
using TMPro;
using UnityEngine;

public class Babarian : MonoBehaviour
{
    [SerializeField] private CharacterDataSO _babarianSo;
    [SerializeField] private GameObject _babarianDialogUI;
    [SerializeField] private float _checkRadius;
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private TextMeshProUGUI _playerText;
    [SerializeField] private PlayerItemSo _playerItemSo;
    [SerializeField] private GameObject _babarianDutorial;
    private string _babarianDialog1 = "내 소중한 망치를 잃어버렸어...";
    private string _babarianDialog2 = "고맙군";
    private string _babarianDialog3 = "이제 모험을 다시 시작할 수 있겠군";
    private string _babarianDialog4 = "나의 망치는 어디에?";
    private string _playerDialog1 = "망치가 없다...";
    private string _playerDialog2 = "어디선가 얻을 수 있을지도?";
    public LayerMask playerLayer;
    private bool _playerFind = false;
    private bool _babarianDutorialFinish;

    Coroutine coroutine;

    private void Update()
    {
        if (_playerFind)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerFind = false;
                GameManager.Instance.StopPlayer();
                _babarianDialogUI.SetActive(true);
                if (_textMeshPro.text == "")
                    coroutine = StartCoroutine(ShowText(_babarianDialog1));
            }
        }
        else if (!_playerFind && coroutine == null)
        {
            _textMeshPro.text = "";
        }
        if (Input.anyKeyDown && _babarianDutorialFinish)
        {
            _babarianDutorial.SetActive(false);
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
        if (!_playerItemSo.haveHammer)
        {
            StartCoroutine(NoHammer());
            return;
        }
        StopAllCoroutines();
        _babarianSo.available = true;
        _babarianDialogUI.SetActive(false);
        StartCoroutine(Yes());
    }

    private IEnumerator NoHammer()
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
    }

    private IEnumerator Yes()
    {

        _textMeshPro.text = "";
        for (int i = 0; i < _babarianDialog2.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _babarianDialog2[i];
        }
        yield return new WaitForSeconds(0.8f);
        _textMeshPro.text = "";
        for (int i = 0; i < _babarianDialog3.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _babarianDialog3[i];
        }
        yield return new WaitForSeconds(0.8f);
        _babarianDutorial.SetActive(true);
        yield return new WaitForSeconds(2f);
        _textMeshPro.text = "";
        _babarianDutorialFinish = true;
    }

    public void NoButton()
    {
        StopAllCoroutines();
        _babarianDialogUI.SetActive(false);
        StartCoroutine(No());
    }

    private IEnumerator No()
    {
        _textMeshPro.text = "";
        for (int i = 0; i < _babarianDialog4.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _textMeshPro.text += _babarianDialog4[i];
        }
        yield return new WaitForSeconds(0.8f);
        _textMeshPro.text = "";
        _playerFind = false;
        GameManager.Instance.MovePlayer();
    }
}
