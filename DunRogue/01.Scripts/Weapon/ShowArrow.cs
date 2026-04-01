using DG.Tweening;
using UnityEngine;

public class ShowArrow : MonoBehaviour
{
    private Bow _bow;
    private Weapon _weapon;

    [SerializeField] private Transform _targetTrm;
    private Tween _chargeTween;
    [SerializeField] private float _chargeAmount = 1f;
    [SerializeField] private float _chargeTime = 0.5f;
    float targetX;


    private void Awake()
    {
        transform.position = _targetTrm.position;
        _bow = FindObjectOfType<Bow>();
        _weapon = FindObjectOfType<Weapon>();
    }



    private void OnEnable()
    {
        _bow.BowCharging += HandleBowCharging;
        _bow.EndBowCharging += HandleEndBowCharging;
    }

    private void OnDisable()
    {
        _bow.BowCharging -= HandleBowCharging;
        _bow.EndBowCharging -= HandleEndBowCharging;
    }

    private void HandleEndBowCharging()
    {

        if (targetX == transform.localPosition.x)
        {
            SoundManager.Instance.PlaySfx(SFXEnum.Arrow);
            transform.localPosition = new Vector3(0, 0, 0);
            _chargeTween.Kill();
            Arrow arrow = PoolManager.Instance.Pop("Arrow") as Arrow;
            arrow.transform.position = transform.position;
            arrow.transform.eulerAngles = new Vector3(0, 0, _weapon.desiredAngle);
            Weapon.isAttack = false;
            _bow.ChangeBow(_bow._bow1);
            gameObject.SetActive(false);
            Bow._isBowCharging = false;
            //StartCoroutine(DelayShoot());
            //Instantiate(_arrowPrefab,transform.position,Quaternion.identity);// poolManager ·Î º¯°æ
        }
        else
        {
            Weapon.isAttack = false;
            gameObject.SetActive(false);
            _bow.ChangeBow(_bow._bow1);
            Bow._isBowCharging = false;
            transform.localPosition = new Vector3(0, 0, 0);
            _chargeTween.Kill();
        }

    }

    //private IEnumerator DelayShoot()
    //{

    //}


    private void HandleBowCharging()
    {
        targetX = _targetTrm.localPosition.x - _chargeAmount;
        _chargeTween = _targetTrm.DOLocalMoveX(targetX, _chargeTime)
               .SetEase(Ease.OutQuint);
        _bow.ChangeBow(_bow._bow2);
    }
}
