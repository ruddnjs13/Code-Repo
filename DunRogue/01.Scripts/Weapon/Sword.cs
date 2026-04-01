using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] Transform _player;
    private Animator _animator;
    public DamageCaster damageCaster { get; private set; }

    private int _rightAttackHash = Animator.StringToHash("RightAttack");
    private int _leftAttackHash = Animator.StringToHash("LeftAttack");
    private int _endAttackkHash = Animator.StringToHash("EndAttack");
    private bool isCoolTime = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }
    private void OnEnable()
    {
        _inputReader.AttackKeyEvent += HandleAttackKeyEvent;
        Weapon.ammo = 0;
        AmmoUI.Instance.SetAmmo();
    }

    private void OnDisable()
    {
        _inputReader.AttackKeyEvent -= HandleAttackKeyEvent;
    }

    private void HandleAttackKeyEvent()
    {
        if (isCoolTime) return;

        if (_inputReader.mousePos.x < _player.position.x)
        {
            StartCoroutine(LeftAttack());
        }
        else
        {
            StartCoroutine(RightAttack());
        }
    }

    private IEnumerator RightAttack()
    {
        isCoolTime = true;
        Weapon.isAttack = true;
        _animator.SetTrigger(_rightAttackHash);
        yield return new WaitForSeconds(0.4f);
        isCoolTime = false;
    }
    private IEnumerator LeftAttack()
    {
        isCoolTime = true;
        Weapon.isAttack = true;
        _animator.SetTrigger(_leftAttackHash);
        yield return new WaitForSeconds(0.4f);
        isCoolTime = false;
    }

    public void Attack()
    {
        damageCaster.CastDamage(Player._weaponDamage);
    }

    public void EndAttack()
    {
        _animator.SetTrigger(_endAttackkHash);
        Weapon.isAttack = false;

    }
}
