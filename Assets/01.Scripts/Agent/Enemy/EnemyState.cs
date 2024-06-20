using System;
using UnityEngine;

public class EnemyState<T> where T : Enum
{
    protected EnemyStateMachine<T> _stateMachine;
    public Enemy _enemyBase;

    protected int _animBoolHash;
    protected bool _endTriggerCalled;

    public EnemyState(Enemy enemy, EnemyStateMachine<T> enemyStateMachine, string animBoolHash)
    {
        _enemyBase = enemy;
        _stateMachine = enemyStateMachine;
        _animBoolHash = Animator.StringToHash(animBoolHash);
    }

    public virtual void UpdateState() { }

    public virtual void Enter()
    {
        _endTriggerCalled = false;
    }

    public virtual void Eixt()
    {
        _endTriggerCalled = true;
    }

    public void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
