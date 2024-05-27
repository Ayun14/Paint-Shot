using UnityEngine;

public enum AnimationType
{
    Idle, Walk
}

public class PlayerAnimation : MonoBehaviour
{
    private string _currentAnimHash = "Idle";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(string animHash)
    {
        _animator.SetBool(_currentAnimHash, false);
        _currentAnimHash = animHash;
        _animator.SetBool(_currentAnimHash, true);
    }
}
