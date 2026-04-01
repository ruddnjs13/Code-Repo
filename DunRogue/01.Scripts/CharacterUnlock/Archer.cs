using System.Collections;
using TMPro;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private bool _playerFind = false;
    [SerializeField] private TextMeshPro _dialog;
    [SerializeField] private GameObject _ArcherDutorial;
    [SerializeField] private CharacterDataSO _archerSO;

    private string _dutorial1 = "이곳은 미지의 영역인 던전이야";
    private string _dutorial2 = "니가 이곳에서 살아남기위한 간단한 시험이 있을거야..";
    private string _dutorial3 = "마우스를 클릭하면 공격을 할 수 있어";
    private string _dutorial4 = "꼭 통과 하길 바래";
    private bool _endDialog;

    private void Update()
    {
        if (_playerFind)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.StopPlayer();
                _playerFind = false;
                StartCoroutine(ShowText());
            }
        }
        if (Input.anyKeyDown && _endDialog)
        {
            _ArcherDutorial.SetActive(false);
            gameObject.SetActive(false);
            GameManager.Instance.MovePlayer();
        }
       
    }


    public IEnumerator ShowText()
    {
        for (int i = 0; i< _dutorial1.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _dialog.text += _dutorial1[i];
        }
        yield return new WaitForSeconds(0.8f);
        _dialog.text = "";  
        for (int i = 0; i< _dutorial2.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _dialog.text += _dutorial2[i];
        }
        yield return new WaitForSeconds(0.8f);
        _dialog.text = "";  
        for (int i = 0; i< _dutorial3.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _dialog.text += _dutorial3[i];
        }
        yield return new WaitForSeconds(0.8f);
        _dialog.text = "";
        for (int i = 0; i< _dutorial4.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _dialog.text += _dutorial4[i];
        }
        yield return new WaitForSeconds(0.8f);
        _dialog.text = "";
        _ArcherDutorial.SetActive(true);
        _archerSO.available = true;
        _endDialog = true;
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
}
