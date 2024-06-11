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
        _enemyBase.EnemyGun.PlayPaintParticle();
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

        if (_enemyBase.EnemyGun.IsCanPaint())
            _enemyBase.EnemyGun.PlayPaintParticle();

        if (!_enemyBase.IsPlayerDetected())
            _enemyBase.StateMachine.ChangeState(EnemyState.Paint);

        if (_enemyBase.player != null)
        {
            if (Vector3.Distance(_enemyBase.player.transform.position, 
                _enemyBase.transform.position) <= _enemyBase.attackDistance)
            {
                _enemyBase.EnemyMovement.SetMovement(Vector3.zero);
            }
            else
            {
                Vector3 dir =
                    _enemyBase.player.transform.position - _enemyBase.transform.position;
                _enemyBase.EnemyMovement.SetMovement(dir.normalized);
            }
        }
    }
}
