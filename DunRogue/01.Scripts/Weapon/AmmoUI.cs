using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoSingleton<AmmoUI>
{
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private Transform _AmmoUI;


    public void SetAmmo()
    {
        foreach(Transform child in _AmmoUI)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < Weapon.ammo; i++)
        {
            Instantiate(_ammoPrefab, _AmmoUI);
        }
    }
}
