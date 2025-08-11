using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class RotateMap2 : MonoBehaviour,IInteractable
{
   private InputReaderSO _inputReader;
    private Transform _rotateAxis;
    private Transform _playerTrm;
    private float _time;
    private GameObject _grid;
    private GameObject _showDirection;
    private Vector2 _originGravity;
    
    private PlayerInteraction _playerInteraction;
    private bool isRotate = false;
    [FormerlySerializedAs("_isInteract")] public bool _canRotate = false;

    private Quaternion _leftRot, _rightRot;

    private void Awake()
    {
        _showDirection = transform.Find("ShowDirection").gameObject;
        _originGravity = Physics2D.gravity;
    }

    private void Start()
    {
        _leftRot = Quaternion.Euler(0, 0, -180f);
        _rightRot = Quaternion.Euler(0, 0, 180f);
        _inputReader.ClockwiseRotateEvent += HandleLeftRotate;
        _inputReader.CounterClockwiseRotateEvent += HandleRightRotate;
    }

    private void HandleRightRotate()
    {
        if (!_canRotate)
        {
            return;
        }

        if (!isRotate)
        {
            
            if (!isRotate)
            {
                isRotate = true;
                RotateManager.Instance.CurrentRotationIdx = (RotateManager.Instance.CurrentRotationIdx + 2) % 4;
                MapRotate(_leftRot);
            }
        }
    }

    private void HandleLeftRotate()
    {
        if (!_canRotate)
        {
            return;
        }
        
        isRotate = true;
        RotateManager.Instance.CurrentRotationIdx -= 2;
        if (RotateManager.Instance.CurrentRotationIdx < 0)
        {
            RotateManager.Instance.CurrentRotationIdx = 3+ RotateManager.Instance.CurrentRotationIdx+1;
        }
        MapRotate(_rightRot);
        
    }

    public void MapRotate(Quaternion direction)
    {
        SoundManager.Instance.PlaySfx(SFXEnum.MapRotate);
        RotateManager.Instance.StartRotateEvent?.Invoke();
        StopAllCoroutines();
        Vector3 previousPos = _grid.transform.position;
        _rotateAxis.transform.position = _playerTrm.position;
        _grid.transform.position = previousPos;
        
        
        Physics2D.gravity = Vector2.zero;
        StartCoroutine(MapRotateCoroutine(direction));
    }

    private IEnumerator MapRotateCoroutine(Quaternion direction)
    {
        Quaternion start = _rotateAxis.transform.rotation;
        float current = 0;
        float percent = 0;
        
        Quaternion target = start * direction;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / _time;

            _rotateAxis.transform.rotation = Quaternion.Lerp(start, target, percent);
            yield return null;
        }


        Physics2D.gravity = _originGravity;
        isRotate = false;
        _canRotate = false;
        RotateManager.Instance.EndRotateEvent?.Invoke();
    }

    public void Interact()
    {
        Debug.Log(transform.localEulerAngles.z);
        Debug.Log(RotateManager.Instance.CurrentRotationIdx);
        Debug.Log(RotateManager.Instance._rotations1[RotateManager.Instance.CurrentRotationIdx]);
        if (transform.localEulerAngles.z ==
            RotateManager.Instance._rotations1[RotateManager.Instance.CurrentRotationIdx])
        {
            _canRotate = true;
            _showDirection.SetActive(true);
        }
    }

    public void EndInteract()
    {
        _canRotate = false;
        _showDirection.SetActive(false);
    }
    
    public void Initialize(Transform playerTrm, Transform rotateAxis, GameObject grid, float time,InputReaderSO inputReaderSo)
    {
        _inputReader = inputReaderSo;
        _playerTrm = playerTrm;
        _rotateAxis = rotateAxis;
        _grid = grid;
        _time = time;
    }
}
