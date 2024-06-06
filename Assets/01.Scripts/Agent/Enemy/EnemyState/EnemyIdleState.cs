using UnityEngine;

public class EnemyIdleState : EnemyState<EnemyState>
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine<EnemyState> enemyStateMachine, string animBoolHash) : base(enemy, enemyStateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.EnemyAnimation.PlayIdleAnimation();
        _enemyBase.EnemyMovement.SetMovement(Vector3.zero);
    }

    public override void Eixt()
    {
        _enemyBase.EnemyAnimation.StopIdleAnimation();
        base.Eixt();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemyBase.IsCanPaint())
            _enemyBase.StateMachine.ChangeState(EnemyState.Paint);

        if (_enemyBase.IsPlayerDetected())
            _enemyBase.StateMachine.ChangeState(EnemyState.Attack);
    }
}
