using UnityEngine;

public class WeaponRendering : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Weapon _weapon;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weapon = GetComponentInParent<Weapon>();
    }

    private void Update()
    {
        FlipWeapon(_weapon.desiredAngle >= 90 || _weapon.desiredAngle <= -90);
        SortingWeapon(_weapon.desiredAngle < 0);
    }

    public void FlipWeapon(bool value)
    {
        int flip = (value ? -2 : 2);

        if (flip == 2)
        {
            transform.localScale = new Vector3(2, flip, 1);
        }
        else if (flip == -2)
        {
            transform.localScale = new Vector3(2, flip, 1);
        }
    }

    public void SortingWeapon(bool value)
    {
        _spriteRenderer.sortingOrder = (value ? 1 : -1);
    }
}
