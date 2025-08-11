using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RotateMap1 : MonoBehaviour,IInteractable
{
    private InputReaderSO _inputReader;
    private Transform _playerTrm;
    private float _time;
    private GameObject _grid;
    private Transform _rotateAxis;
    private GameObject _showDirection;
    
    private PlayerInteraction _playerInteraction;
    private bool isRotate = false;
    public bool _canRotate = false;

    private Quaternion _leftRot, _rightRot;

    private void Awake()
    {
        _showDirection = transform.Find("ShowDirection").gameObject;
    }

    private void Start()
    {
        _leftRot = Quaternion.Euler(0, 0, -90f);
        _rightRot = Quaternion.Euler(0, 0, 90f);
        _inputReader.ClockwiseRotateEvent += HandleLeftRotate;
        _inputReader.CounterClockwiseRotateEvent += HandleRightRotate;
    }

    private void OnDisable()
    {
        _inputReader.ClockwiseRotateEvent -= HandleLeftRotate;
        _inputReader.CounterClockwiseRotateEvent -= HandleRightRotate;
    }

    private void HandleRightRotate()
    {
        if (!_canRotate)
        {
            return;
        }
        
        if (!isRotate)
        {
            isRotate = true;
            RotateManager.Instance.CurrentRotationIdx = --RotateManager.Instance.CurrentRotationIdx;
            if (RotateManager.Instance.CurrentRotationIdx < 0)
            {
                RotateManager.Instance.CurrentRotationIdx = 3;
            }
            MapRotate(_rightRot);
        }
    }

    private void HandleLeftRotate()
    {
        if (!_canRotate)
        {
            return;
        }

        if (!isRotate)
        {
            isRotate = true;
            RotateManager.Instance.CurrentRotationIdx = ++RotateManager.Instance.CurrentRotationIdx % 4;
            MapRotate(_leftRot);
        }

}


    private void Update()
    {
        ChooseDirectionAndStart();
    }

    private void ChooseDirectionAndStart()
    {
        
    }

    private void MapRotate(Quaternion direction)
    {
        SoundManager.Instance.PlaySfx(SFXEnum.MapRotate);
        RotateManager.Instance.StartRotateEvent?.Invoke();
        StopAllCoroutines();
        Vector3 previousPos = _grid.transform.position;
        _rotateAxis.transform.position = _playerTrm.position;
        _grid.transform.position = previousPos;
        Physics2D.gravity = new Vector2(0, 0);
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
        

        Physics2D.gravity = new Vector2(0, -9.81f);
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

    public void Initialize(Transform playerTrm, Transform rotateAxis, GameObject grid, float time,InputReaderSO inputReader)
    {
        _inputReader = inputReader;
        _playerTrm = playerTrm;
        _rotateAxis = rotateAxis;
        _grid = grid;
        _time = time;
    }
}
