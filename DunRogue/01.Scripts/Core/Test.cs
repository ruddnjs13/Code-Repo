using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(_enemyPrefab,transform.position,Quaternion.identity);
        }
    }
}
