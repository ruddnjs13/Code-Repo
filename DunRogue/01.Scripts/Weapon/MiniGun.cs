using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MiniGun : MonoBehaviour
{
    Tween _recoilTween;
    [SerializeField] private float _recoilAmount;
    [SerializeField] private float _recoilTime;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _firePos;

    private bool isRecoil = false;

    private int _maxAmmo = 40;
    public int _ammo;

    private void OnEnable()
    {
        _inputReader.AttackKeyEvent += HandleAttackKeyEvent;
        _inputReader.EndAttackKeyEvent += HandleEndAttackKeyEvent;
        _inputReader.ReloadEvent += HandleReloadEvent;
    }
    private void OnDisable()
    {
        _inputReader.AttackKeyEvent -= HandleAttackKeyEvent;
        _inputReader.EndAttackKeyEvent -= HandleEndAttackKeyEvent;
        _inputReader.ReloadEvent -= HandleReloadEvent;
    }



    private void Start()
    {
        _ammo = _maxAmmo;
    }

    private void HandleReloadEvent()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        _ammo = _maxAmmo;
        StopCoroutine(Reload());
    }



    private void HandleEndAttackKeyEvent()
    {
        StopAllCoroutines();
    }

    private void HandleAttackKeyEvent()
    {
        StartCoroutine(TryShoot());
    }

    private IEnumerator TryShoot()
    {
        while (_ammo > 0)
        {
            if (!isRecoil)
            {
                isRecoil = true;
                Recoil();
            }
            Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
            bullet.transform.position = _firePos.position;
            bullet.transform.rotation = _firePos.rotation;
            _ammo--;
            yield return new WaitForSeconds(0.1f);
            isRecoil = false;
        }
        StopCoroutine(TryShoot());
    }

    public void Recoil()
    {
        float targetX = transform.localPosition.x - _recoilAmount;
        _recoilTween = transform.DOLocalMoveX(targetX, _recoilTime)
                .SetEase(Ease.OutCirc)
                .SetLoops(2, LoopType.Yoyo);
    }
}
