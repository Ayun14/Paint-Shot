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

        if (_enemyBase.EnemyGun.IsNonePaint())
        {
            if (_enemyBase.EnemyGun.IsCanPaint())
                _enemyBase.EnemyGun.PlayPaintParticle();
            else
                _enemyBase.EnemyGun.StopPaintParticle();
        }

        if (!_enemyBase.IsTargetDetected())
            _enemyBase.StateMachine.ChangeState(EnemyState.Paint);

        if (_enemyBase.target != null)
        {
            if (Vector3.Distance(_enemyBase.target.transform.position,
                _enemyBase.transform.position) <= _enemyBase.attackDistance)
            {
                _enemyBase.EnemyMovement.SetMovement(Vector3.zero);
            }
            else
            {
                Vector3 dir =
                    _enemyBase.target.transform.position - _enemyBase.transform.position;
                _enemyBase.EnemyMovement.SetMovement(dir.normalized);
            }
        }
    }
}
