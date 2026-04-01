using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Player : Agent
{
    [SerializeField] private CharacterDataSO _archerData;
    [SerializeField] private CharacterDataSO _knightData;
    [SerializeField] private CharacterDataSO _dinoData;
    [SerializeField] private CharacterDataSO _babarianData;
    [SerializeField] private InputReader _inputReader;
    public SpriteLibrary _library;

    public static int _weaponDamage = 40;

    public static bool IsCombat = false;

    private AgentMove _agentMove;
    private PlayerUI _playerUI;

    public event Action<string> WeaponChanged;

    protected override void Awake()
    {
        base.Awake();
        _agentMove = GetComponent<AgentMove>();
        _library = FindObjectOfType<SpriteLibrary>();
        _playerUI = GetComponent<PlayerUI>();
        _inputReader.PlayerChangeEvent += HandlePlayerChangeEvent;
        IsCombat = false;
    }
    private void Start()
    {
        _library.spriteLibraryAsset = _knightData.library;
    }
    private void Update()
    {
        _agentMove.SetMovement(_inputReader.movement);
    }

    private void FixedUpdate()
    {
        Flip(_inputReader.mousePos);
    }

    private void OnDisable()
    {
        _inputReader.PlayerChangeEvent -= HandlePlayerChangeEvent;
    }

    private void HandlePlayerChangeEvent(int charIdx)
    {
        if (Weapon.isAttack || Weapon.IsReloading) return;
        switch (charIdx)
        {
            case 1:
                _library.spriteLibraryAsset = _knightData.library;
                _weaponDamage = _knightData.damage;
                _playerUI.SetCharacter(1);
                WeaponChanged?.Invoke(_knightData.weapon);
                break;
            case 2:
                if (!_archerData.available) return;
                _library.spriteLibraryAsset = _archerData.library;
                _weaponDamage = _archerData.damage;
                _playerUI.SetCharacter(2);
                WeaponChanged?.Invoke(_archerData.weapon);
                break;
            case 4:
                if (!_babarianData.available) return;
                _library.spriteLibraryAsset = _babarianData.library;
                _weaponDamage = _babarianData.damage;
                _playerUI.SetCharacter(3);
                WeaponChanged?.Invoke(_babarianData.weapon);
                break;
            case 3:
                if (!_dinoData.available) return;
                _library.spriteLibraryAsset = _dinoData.library;
                _weaponDamage = _dinoData.damage;
                _playerUI.SetCharacter(4);
                WeaponChanged?.Invoke(_dinoData.weapon);
                break;
        }

    }
    public void OnDead()
    {
        if (isDead) return;
        isDead = true;
        GameManager.Instance.SetDieUI(this);
    }
}
