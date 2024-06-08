using UnityEngine;

public class EnemyPaintState : EnemyState<EnemyState>
{
    private float _currentTime;
    private float _dirChangeMinTime = 0.8f;
    private float _dirChangeMaxTime = 1.5f;
    private float _dirChangeTime;

    public EnemyPaintState(Enemy enemy, EnemyStateMachine<EnemyState> enemyStateMachine, string animBoolHash) : base(enemy, enemyStateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.EnemyAnimation.PlayPaintAnimation();
        _enemyBase.EnemyGun.PlayPaintParticle();

        _enemyBase.ChangeRandomDirection();
        _dirChangeTime = Random.Range(_dirChangeMinTime, _dirChangeMaxTime);
    }

    public override void Eixt()
    {
        _enemyBase.EnemyAnimation.StopPaintAnimation();
        _enemyBase.EnemyGun.StopPaintParticle();

        base.Eixt();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        ChangeDirection();
        CheckPlayerInAttackRange();
    }

    private void ChangeDirection()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dirChangeTime || _enemyBase.IsObstacleInFront())
        {
            _enemyBase.ChangeRandomDirection();
            _currentTime = 0;
        }
    }

    private void CheckPlayerInAttackRange()
    {
        _enemyBase.player = _enemyBase.IsPlayerDetected();
        if (_enemyBase.player)
        {
            _enemyBase.StateMachine.ChangeState(EnemyState.Attack);
        }
    }
}
