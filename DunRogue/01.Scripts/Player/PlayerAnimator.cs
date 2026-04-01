using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputReader _playerInput;
    private Animator _animator;

    private int _velocityHash = Animator.StringToHash("Velocity");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetRunning();
    }

    private void SetRunning()
    {
        _animator.SetFloat(_velocityHash, Mathf.Abs(_playerInput.movement.x) + Mathf.Abs(_playerInput.movement.y));
    }
}
