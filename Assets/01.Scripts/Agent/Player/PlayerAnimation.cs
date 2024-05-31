using UnityEngine;

public enum AnimationType
{
    Idle, Walk
}

public class PlayerAnimation : MonoBehaviour
{
    private string _currentAnimHash = "Idle";

    private Animator _animator;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Start()
    {
        _playerInput.OnFireEvent += PlayFireAnimation;
        _playerInput.OnFireStopEvent += StopFireAnimation;
    }

    private void OnDestroy()
    {
        _playerInput.OnFireEvent -= PlayFireAnimation;
        _playerInput.OnFireStopEvent -= StopFireAnimation;
    }

    public void ChangeAnimation(string animHash)
    {
        _animator.SetBool(_currentAnimHash, false);
        _currentAnimHash = animHash;
        _animator.SetBool(_currentAnimHash, true);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetLayerWeight(1, 1);
    }

    public void StopIdleAnimation()
    {
        _animator.SetLayerWeight(1, 0);
    }

    public void PlayRunAnimation()
    {
        _animator.SetLayerWeight(2, 1);
    }

    public void StopRunAnimation()
    {
        _animator.SetLayerWeight(2, 0);
    }

    public void PlayFireAnimation()
    {
        _animator.SetLayerWeight(3, 1);
    }

    public void StopFireAnimation()
    {
        _animator.SetLayerWeight(3, 0);
    }
}
