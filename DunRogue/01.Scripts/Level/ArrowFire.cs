using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArrowFire : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    private float _maxTime = 1.5f;
    private float _minTime = 3.5f;

    private void Start()
    {
        StartCoroutine(ShootArrow());
    }

    private IEnumerator ShootArrow()
    {
        while (true)
        {
            EnemyArrow arrow = PoolManager.Instance.Pop("EnemyArrow") as EnemyArrow;
            arrow.transform.SetPositionAndRotation(_firePos.position, _firePos.rotation);
            yield return new WaitForSeconds(Random.Range(_minTime, _maxTime));
        }
    }
}

