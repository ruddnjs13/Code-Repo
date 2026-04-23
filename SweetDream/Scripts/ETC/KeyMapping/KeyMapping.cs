using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class KeyMapping : MonoSingleton<KeyMapping>
{
    private Controls _controls;
    public string RebindInfo {get; private set;}
    [SerializeField] private InputReaderSO inputReaderSO;
    [SerializeField] private List<TextMeshProUGUI> moveKeyBindingTexts;
    [SerializeField] private TextMeshProUGUI jumpKeyBindingText;
    [SerializeField] private TextMeshProUGUI interactKeyBindingTexts;
    [SerializeField] private TextMeshProUGUI clockwiseKeyBindingTexts;
    [SerializeField] private TextMeshProUGUI counterClockwiseKeyBindingTexts;
    public KeyMappingSaveLord saveLord;
    
    private bool _isRebinding = false;
    
    private void Start()
    {
        InitInputSetting();
    }

    private void InitInputSetting()
    {
        saveLord.LoadKeyDataFromJson();
        RebindInfo = saveLord.KeyData.keyString;
        _controls = new Controls();
        _controls.Player.Enable();
        _controls.LoadBindingOverridesFromJson(RebindInfo);
        
        moveKeyBindingTexts[0].text = _controls.Player.Movement.GetBindingDisplayString(4);
        moveKeyBindingTexts[1].text = _controls.Player.Movement.GetBindingDisplayString(3);

        
        jumpKeyBindingText.text = _controls.Player.Jump.GetBindingDisplayString(0);
        interactKeyBindingTexts.text = _controls.Player.Interaction.GetBindingDisplayString(0);               
        clockwiseKeyBindingTexts.text = _controls.Player.MapRotateClockwise.GetBindingDisplayString(0);               
        counterClockwiseKeyBindingTexts.text = _controls.Player.MapRotateCounterClockwise.GetBindingDisplayString(0);               

    }
    
    public void MovementRebinding(int bindIdx)
    {
        if(_isRebinding) return;
        _isRebinding = true;
        moveKeyBindingTexts[4-bindIdx].text = "New Key";
        _controls.Player.Disable();

        _controls.Player.Movement.PerformInteractiveRebinding(bindIdx)
            .WithControlsExcluding("Mouse")
            .OnComplete( op =>
            {
                RebindInfo = _controls.SaveBindingOverridesAsJson();
                saveLord.KeyData.keyString = RebindInfo;
                saveLord.SaveKeyDataToJson();
                _controls.LoadBindingOverridesFromJson(RebindInfo);
                 moveKeyBindingTexts[4-bindIdx].text =  _controls.Player.Movement.GetBindingDisplayString(bindIdx);
                 inputReaderSO.RebindInputReader(RebindInfo);
                 Debug.Log(RebindInfo);
                 _isRebinding = false;
            })
            .OnCancel(op =>
            {
                op.Dispose();
                _isRebinding = false;
            }).Start();
        _controls.Player.Enable();

    }
    public void JumpRebinding()
    {
        if(_isRebinding) return;
        _isRebinding = true;
        
        jumpKeyBindingText.text = "New Key";
        _controls.Player.Disable();

        _controls.Player.Jump.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnComplete( op =>
            {
                RebindInfo = _controls.SaveBindingOverridesAsJson();
                saveLord.KeyData.keyString = RebindInfo;
                saveLord.SaveKeyDataToJson();
                _controls.LoadBindingOverridesFromJson(RebindInfo);
                jumpKeyBindingText.text =  _controls.Player.Jump.GetBindingDisplayString();
                _isRebinding = false;
                inputReaderSO.RebindInputReader(RebindInfo);
            })
            .Start()
            .OnCancel(op =>
            {
                op.Dispose();
                _isRebinding = false;
            }).Start();
        _controls.Player.Enable();
    } 
    public void InteractRebinding()
    {
        if(_isRebinding) return;
        _isRebinding = true;
        
        interactKeyBindingTexts.text = "New Key";
        _controls.Player.Disable();

        _controls.Player.Interaction.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnComplete( op =>
            {
                RebindInfo = _controls.SaveBindingOverridesAsJson();
                saveLord.KeyData.keyString = RebindInfo;
                saveLord.SaveKeyDataToJson();
                _controls.LoadBindingOverridesFromJson(RebindInfo);
                interactKeyBindingTexts.text =  _controls.Player.Interaction.GetBindingDisplayString();
                _isRebinding = false;
                inputReaderSO.RebindInputReader(RebindInfo);
            })
            .OnCancel(op =>
            {
                op.Dispose();
                _isRebinding = false;
            }).Start();
        _controls.Player.Enable();
    }
    
    public void MapRotateClockwiseRebinding()
    {
        if(_isRebinding) return;
        _isRebinding = true;
        
        clockwiseKeyBindingTexts.text = "New Key";
        _controls.Player.Disable();

        _controls.Player.MapRotateClockwise.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnComplete( op =>
            {
                RebindInfo = _controls.SaveBindingOverridesAsJson();
                saveLord.KeyData.keyString = RebindInfo;
                saveLord.SaveKeyDataToJson();
                _controls.LoadBindingOverridesFromJson(RebindInfo);
                clockwiseKeyBindingTexts.text =  _controls.Player.MapRotateClockwise.GetBindingDisplayString();
                _isRebinding = false;
                inputReaderSO.RebindInputReader(RebindInfo);
            })
            .OnCancel(op =>
            {
                op.Dispose();
                _isRebinding = false;
            }).Start();
        _controls.Player.Enable();
    }
    
    public void MapRotateCounterClockwiseRebinding()
    {
        if(_isRebinding) return;
        _isRebinding = true;
        
        counterClockwiseKeyBindingTexts.text = "New Key";
        _controls.Player.Disable();

        _controls.Player.MapRotateCounterClockwise.PerformInteractiveRebinding(0)
            .WithControlsExcluding("Mouse")
            .OnComplete( op =>
            {
                RebindInfo = _controls.SaveBindingOverridesAsJson();
                saveLord.KeyData.keyString = RebindInfo;
                saveLord.SaveKeyDataToJson();
                _controls.LoadBindingOverridesFromJson(RebindInfo);
                counterClockwiseKeyBindingTexts.text =  _controls.Player.MapRotateCounterClockwise.GetBindingDisplayString();
                _isRebinding = false;
                inputReaderSO.RebindInputReader(RebindInfo);
            })
            .OnCancel(op =>
            {
                op.Dispose();
                _isRebinding = false;
            }).Start();
        _controls.Player.Enable();
    }
}
