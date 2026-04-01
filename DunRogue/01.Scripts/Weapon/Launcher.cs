using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _firePos;
    private int _maxAmmo = 2;
    private int _ammo = 2;
    private bool _isCoolTime = false;

    private Tween _recoilTween;
    [SerializeField] private float _recoilAmount;
    [SerializeField] private float _recoilTime;
    public bool _isReload { get; private set; }

    private void Awake()
    {
    }
    private void OnEnable()
    {
        _inputReader.AttackKeyEvent += HandleAttackKeyEvent;
        _inputReader.ReloadEvent += HandleReloadEvent;
        Weapon.ammo = _ammo;
        AmmoUI.Instance.SetAmmo();
    }



    private void OnDisable()
    {
        _inputReader.AttackKeyEvent -= HandleAttackKeyEvent;
        _inputReader.ReloadEvent -= HandleReloadEvent;
        StopAllCoroutines();
    }

    private void Update()
    {
    }

    private void HandleReloadEvent()
    {
        if (_ammo == 2 || Weapon.IsReloading) return;
        StartCoroutine(Reload());
    }
    private void HandleAttackKeyEvent()
    {
        if (_ammo > 0 && !_isCoolTime)
        {
            SoundManager.Instance.PlaySfx(SFXEnum.Rocket);
            Weapon.isAttack = true;
            Recoil();
            _isCoolTime = true;
            _ammo--;
            Weapon.ammo = _ammo;
            AmmoUI.Instance.SetAmmo();
            Rocket rocket = PoolManager.Instance.Pop("Rocket") as Rocket;
            rocket.transform.position = _firePos.position;
            rocket.transform.rotation = _firePos.rotation;
            StartCoroutine(ShootCoolTime());
        }
    }

    private IEnumerator ShootCoolTime()
    {
        yield return new WaitForSeconds(1.6f);
        _isCoolTime = false;
        Weapon.isAttack = false;
    }

    public void Recoil()
    {
        float targetX = transform.localPosition.x - _recoilAmount;
        _recoilTween = transform.DOLocalMoveX(targetX, _recoilTime)
                .SetEase(Ease.OutCirc)
                .SetLoops(2, LoopType.Yoyo);
    }

    private IEnumerator Reload()
    {
        Weapon.IsReloading = true;
        SoundManager.Instance.PlaySfx(SFXEnum.Reload);
        yield return new WaitForSeconds(2);
        _ammo = _maxAmmo;
        Weapon.ammo = _ammo;
        AmmoUI.Instance.SetAmmo();
        Weapon.IsReloading = false;
    }

}
