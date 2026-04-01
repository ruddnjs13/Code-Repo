using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private EnemyWeaponRendering _weaponRendering;
    public  float desireAngle;

    private void Awake()
    {
        _weaponRendering = transform.Find("Weapon").GetComponent<EnemyWeaponRendering>();
    }

    private void Update()
    {
        RotateWeapon();
        _weaponRendering.FlipWeapon(desireAngle >= 90 || desireAngle <= -90);
        _weaponRendering.SortingWeapon(desireAngle < 0);
    }

    private void RotateWeapon()
    {
        Vector3 aimDir = GameManager.Instance.Player.transform.position - transform.position;
        desireAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(desireAngle, Vector3.forward);
    }
}
