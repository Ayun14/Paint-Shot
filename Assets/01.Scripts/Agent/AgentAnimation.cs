using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    Idle, Run, Death
}

public class AgentAnimation : MonoBehaviour
{
    protected string _currentAnimHash = AnimationType.Idle.ToString();
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(string animHash)
    {
        if (_currentAnimHash == animHash) return;

        _animator.SetBool(_currentAnimHash, false);
        _currentAnimHash = animHash;
        _animator.SetBool(_currentAnimHash, true);
    }

    public void DeathAnimation()
    {
        StopIdleAnimation();
        StopRunAnimation();
        StopPaintAnimation();
        ChangeAnimation(AnimationType.Death.ToString());
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

    public void PlayPaintAnimation()
    {
        _animator.SetLayerWeight(3, 1);
    }

    public void StopPaintAnimation()
    {
        _animator.SetLayerWeight(3, 0);
    }
}
