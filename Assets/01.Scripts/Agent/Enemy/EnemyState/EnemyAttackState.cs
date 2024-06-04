using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState<EnemyState>
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine<EnemyState> enemyStateMachine, string animBoolHash) : base(enemy, enemyStateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.EnemyAnimation.PlayPaintAnimation();
    }

    public override void Eixt()
    {
        _enemyBase.EnemyAnimation.StopPaintAnimation();
        base.Eixt();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!_enemyBase.IsPlayerDetected())
            _enemyBase.StateMachine.ChangeState(EnemyState.Paint);
    }
}
