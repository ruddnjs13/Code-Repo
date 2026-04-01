using DG.Tweening;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotGun : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _firePos;
    private int _maxAmmo = 4;
    private int _ammo = 4;
    private bool _isCoolTime = false;

    private Tween _recoilTween;
    [SerializeField] private float _recoilAmount;
    [SerializeField] private float _recoilTime;
    private float _spreadAngle = 24f;

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

    private void HandleReloadEvent()
    {
        if (Weapon.IsReloading || _ammo == 4) return;
        StartCoroutine(Reload());
    }
    private void HandleAttackKeyEvent()
    {
        if (_ammo > 0 && !_isCoolTime)
        {
            SoundManager.Instance.PlaySfx(SFXEnum.ShootGun);
            Weapon.isAttack = true;
            Recoil();
            _isCoolTime = true;
            _ammo--;
            Weapon.ammo = _ammo;
            AmmoUI.Instance.SetAmmo();
            for (int i = 0; i < 4; i++)
            {
                ShootBullet();
            }
            StartCoroutine(ShootCoolTime());
        }
    }

    private void ShootBullet()
    {
        float spreadAngle = Random.Range(-_spreadAngle, _spreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spreadAngle));

        Bullet bullet = PoolManager.Instance.Pop("Bullet") as Bullet;

        bullet.transform.SetPositionAndRotation(_firePos.position, _firePos.rotation * bulletSpreadRotation);
    }

    private IEnumerator ShootCoolTime()
    {
        yield return new WaitForSeconds(0.8f);
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
        yield return new WaitForSeconds(1.2f);
        _ammo = _maxAmmo;
        Weapon.ammo = _ammo;
        AmmoUI.Instance.SetAmmo();Weapon.IsReloading = false;
    }

}
