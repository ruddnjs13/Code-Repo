using System;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private SpriteRenderer _spriteRenderer;

    [field: SerializeField] public Sprite _bow1 { get; private set; }
    [field: SerializeField] public Sprite _bow2 { get; private set; }
    [SerializeField] private GameObject _arrow;
    public event Action BowCharging;
    public static bool _isBowCharging = false;


    public event Action EndBowCharging;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _inputReader.AttackKeyEvent += HandleAttackKeyEvent;
        _inputReader.EndAttackKeyEvent += HandleEndAttackKey;
        Weapon.ammo = 0;
        AmmoUI.Instance.SetAmmo();
    }

    private void OnDisable()
    {
        _inputReader.AttackKeyEvent -= HandleAttackKeyEvent;
        _inputReader.EndAttackKeyEvent -= HandleEndAttackKey;
    }

    private void HandleAttackKeyEvent()
    {
        if (!_isBowCharging)
        {
            Weapon.isAttack = true;
            _arrow.SetActive(true);
            BowCharging?.Invoke();
            _isBowCharging = true;
        }
    }

    private void HandleEndAttackKey()
    {
        EndBowCharging?.Invoke();
    }

    public void ChangeBow(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

}
