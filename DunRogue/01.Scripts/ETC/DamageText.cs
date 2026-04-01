using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;
using System;

public class DamageText : MonoBehaviour, IPoolable
{
    [SerializeField] private float blinkSpeed;
    TextMeshPro textMeshPro;
    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        ResetItem();
    }


    public void ResetItem()
    {
        textMeshPro.alpha = 1f;
    }

    void Update()
    {
        textMeshPro.alpha = Mathf.Lerp(textMeshPro.alpha, 0, Time.deltaTime * blinkSpeed);
    }

    public void PlayDamageText(int damage, Vector3 position, Color color)
    {
        textMeshPro.color = color;
        textMeshPro.text = damage.ToString();
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, Quaternion.identity);
        StartCoroutine(DelayPush());
      
    }

    private IEnumerator DelayPush()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        PoolManager.Instance.Push(this);
    }
}
