using UnityEngine;

public class BossrUI : MonoBehaviour
{
    private Health health;
    [SerializeField] private GameObject BossUI;
    [SerializeField] private GameObject HpFill;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        HpFill.transform.localScale = new Vector3((float)health._currentHealth / 1000f, 1, 1);
    }

    public void OffBossHp()
    {
        BossUI.SetActive(false);
    }
}
