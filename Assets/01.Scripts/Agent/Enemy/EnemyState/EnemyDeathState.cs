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

    public override void UpdateState()
    {
        base.UpdateState();

        _enemyBase.currentSpawnDelayTime += Time.deltaTime;
        if (_enemyBase.currentSpawnDelayTime > _enemyBase.spawnDelayTime)
        {
            _enemyBase.currentSpawnDelayTime = 0;
            _enemyBase.transform.position = _enemyBase.spawnPos;
            _enemyBase.colliderCompo.enabled = true;
            _enemyBase.EnemyMovement.StopImmediately();
            _enemyBase.EnemyHealth.HealthReset();
            _enemyBase.StateMachine.ChangeState(EnemyState.Paint);
        }
    }
}
