using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] Transform _player;
    private Animator _animator;

    private int _rightAttackHash = Animator.StringToHash("RightAttack");
    private int _leftAttackHash = Animator.StringToHash("LeftAttack");
    private int _endAttackkHash = Animator.StringToHash("EndAttack");

    private bool isCooltime = false;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.AttackKeyEvent += HandleAttackKeyEvent;
    }

    private void OnDisable()
    {
        _inputReader.AttackKeyEvent -= HandleAttackKeyEvent;
    }

    private void HandleAttackKeyEvent()
    {
        if (isCooltime) return;
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
        Weapon.isAttack = true;
        isCooltime = true;
        _animator.SetTrigger(_rightAttackHash);
        yield return new WaitForSeconds(0.4f);
        isCooltime = false;
        Weapon.isAttack = false;
    }
    private IEnumerator LeftAttack()
    {
        Weapon.isAttack = true;
        isCooltime = true;
        _animator.SetTrigger(_leftAttackHash);
        yield return new WaitForSeconds(0.4f);
        isCooltime = false;
        Weapon.isAttack = false;
    }

    public void EndAttack()
    {
        _animator.SetTrigger(_endAttackkHash);
    }
}
