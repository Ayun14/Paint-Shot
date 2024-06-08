using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState<EnemyState>
{
    public EnemyDeathState(Enemy enemy, EnemyStateMachine<EnemyState> enemyStateMachine, string animBoolHash) : base(enemy, enemyStateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _enemyBase.EnemyGun.StopPaintParticle();
    }

    public override void Eixt()
    {
        base.Eixt();
    }
}
