using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class StartCradit : MonoBehaviour
{
    [SerializeField] private float startDelay, typingDelay, nextDelay, lastDelay;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private List<string> _texts;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private string titleText;
    
    private Sequence _seq;
    
    private Coroutine _coroutine;
    
    private void Start()
    {
        mainText.text = "";
        targetText.text = "";
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(TextPrint());
    }

    private IEnumerator TextPrint()
    {
        yield return new WaitForSecondsRealtime(startDelay);
        foreach (var text in _texts)
        {
            StartCoroutine(TextTyping(text));
            yield return new WaitForSecondsRealtime(nextDelay);
            StartCoroutine(TextDelete(text));
            yield return new WaitForSecondsRealtime(nextDelay);
            targetText.text = "";
        }
        
        yield return new WaitForSecondsRealtime(lastDelay);

        _seq = DOTween.Sequence()
            .Append(mainText.DOFade(0f, 0.2f))
            .Append(mainText.DOFade(1f, 3f).OnStart(()=>mainText.text = titleText))
            .AppendInterval(3f)
            .Append(mainText.DOFade(0f, 1f));

        _seq.OnComplete(() => SceneManager.LoadScene(1));
    }

    private IEnumerator TextTyping(string text)
    {
        foreach (var t in text)
        {
            targetText.text += t;
            yield return new WaitForSeconds(typingDelay);
        }
    }

    private IEnumerator TextDelete(string text)
    {
        int length = text.Length;

        while (length >= 0)
        {
            string t = text.Substring(0, length);
            targetText.text = t; 
            yield return new WaitForSeconds(typingDelay);
            length--;
        }
    }
}