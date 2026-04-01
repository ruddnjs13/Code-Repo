using System.Collections.Generic;
using UnityEngine;
using Input = UnityEngine.Input;

public class Weapon : MonoBehaviour
{
    [SerializeField] InputReader _playerInput;
    private Player _player;
    private Vector2 _mousePos;

    public static int ammo = 0;

    public static bool isAttack = false;
    public static bool IsReloading = false;

    [Header("Weapons")]
    [SerializeField] private GameObject Bow;
    [SerializeField] private GameObject Sword;
    [SerializeField] private GameObject Launcher;
    [SerializeField] private GameObject MiniGun;
    Dictionary<string, GameObject> WeaponList = new Dictionary<string, GameObject>();

    public float desiredAngle { get; private set; }

    private void Awake()
    {

        _player = FindObjectOfType<Player>();
        _player.WeaponChanged += HandlePlayerChnageEvent;
    }



    private void OnDisable()
    {
        _player.WeaponChanged -= HandlePlayerChnageEvent;
    }

    private void HandlePlayerChnageEvent(string weaponName)
    {
        if (Weapon.isAttack || Weapon.IsReloading) return;
        foreach (string key in WeaponList.Keys)
        {
            WeaponList[key].SetActive(false);
        }
        WeaponList[weaponName].SetActive(true);
    }

    private void Start()
    {
        WeaponList.Add("Bow", Bow);
        WeaponList.Add("Sword", Sword);
        WeaponList.Add("Launcher", Launcher);
        WeaponList.Add("ShotGun", MiniGun);
        WeaponList["Sword"].SetActive(true);
    }

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateGun();
    }

    void RotateGun()
    {
        Vector3 aimDir = (Vector3)_mousePos - transform.position;
        desiredAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
    }

    public Vector2 GetMousePos()
    {
        return _playerInput.mousePos;
    }


}
