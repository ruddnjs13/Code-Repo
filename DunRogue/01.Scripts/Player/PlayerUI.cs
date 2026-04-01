using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Health health;
    [SerializeField] private GameObject HpFill;
    [SerializeField] private GameObject CharacterImage;
    private Image image;
    [SerializeField] private Sprite Knight;
    [SerializeField] private Sprite Archer;
    [SerializeField] private Sprite Babarian;
    [SerializeField] private Sprite Dino;

    private void Awake()
    {
        health = GetComponent<Health>();
        image = CharacterImage.GetComponent<Image>();
    }

    private void Update()
    {
        HpFill.transform.localScale = new Vector3((float)health._currentHealth / 200f, 1, 1);
    }

    public void SetCharacter(int value)
    {
        switch (value)
        {
            case 1:
                image.sprite = Knight;
                break;
            case 2:
                image.sprite = Archer;
                break;
            case 3:
                image.sprite = Babarian;
                break;
            case 4:
                image.sprite = Dino;
                break;
        }
    }
}
