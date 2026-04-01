using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private DamageCaster _damageCaster;
    private bool _inPlayer = false;

    private void Awake()
    {
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inPlayer = true;
            StartCoroutine(OnDamaged());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _inPlayer = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator OnDamaged()
    {
        while (_inPlayer)
        {
            _damageCaster.CastDamage(10);
            yield return new WaitForSeconds(1f);
        }
    }
}
