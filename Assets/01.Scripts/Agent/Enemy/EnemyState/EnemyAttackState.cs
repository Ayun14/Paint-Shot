using UnityEngine;

public class EnemyAttackState : EnemyState<EnemyState>
{
    // stop attack
    private float _currentAttackTime;
    private float _stopAttackMinTime = 1f;
    private float _stopAttackMaxTime = 1.7f;
    private float _stopAttackTime;

    // change direction
    private float _currentTime;
    private float _dirChangeMinTime = 0.8f;
    private float _dirChangeMaxTime = 1.5f;
    private float _dirChangeTime;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine<EnemyState> enemyStateMachine, string animBoolHash) : base(enemy, enemyStateMachine, animBoolHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _currentAttackTime = 0;
        _currentTime = 0;

        _enemyBase.EnemyAnimation.PlayPaintAnimation();
        _enemyBase.EnemyGun.PlayPaintParticle();

        _stopAttackTime = Random.Range(_stopAttackMinTime, _stopAttackMaxTime);
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

        if (!_enemyBase.IsTargetDetected())
            _enemyBase.StateMachine.ChangeState(EnemyState.Paint);

        Paintint();
        Attack();
    }

    private void Paintint()
    {
        if (_enemyBase.EnemyGun.IsNonePaint())
        {
            if (_enemyBase.EnemyGun.IsCanPaint())
                _enemyBase.EnemyGun.PlayPaintParticle();
            else
                _enemyBase.EnemyGun.StopPaintParticle();
        }
    }

    private void Attack()
    {
        if (_enemyBase.target != null)
        {
            _currentAttackTime += Time.deltaTime;
            if (_currentAttackTime >= _stopAttackTime)
            {
                ChangeDirection();
            }
            else
            {
                if (Vector3.Distance(_enemyBase.target.transform.position,
                    _enemyBase.transform.position) <= _enemyBase.attackDistance)
                {
                    Vector3 dir =
                        _enemyBase.target.transform.position - _enemyBase.transform.position;
                    _enemyBase.EnemyMovement.SetLookRotation(dir);
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

    private void ChangeDirection()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dirChangeTime)
        {
            _enemyBase.ChangeRandomDirection();
            _currentTime = 0;
        }

        if (_enemyBase.IsObstacleInFront())
            _enemyBase.EnemyMovement.SetMovement(-_enemyBase.EnemyMovement.Velocity);
    }
}
