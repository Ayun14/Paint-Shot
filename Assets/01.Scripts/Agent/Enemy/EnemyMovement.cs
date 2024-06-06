using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float moveSpeed;

    private Vector3 _velocity = Vector3.zero;
    public Vector3 Velocity => _velocity;

    private Quaternion _targetRotation;

    private Rigidbody _rigid;
    private AgentAnimation _enemyAnimation;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _enemyAnimation = transform.Find("Visual").GetComponent<AgentAnimation>();
    }

    protected void FixedUpdate()
    {
        ApplyRotation();
        Move();
    }

    private void ApplyRotation()
    {
        float rotateSpeed = 8f;
        transform.rotation = Quaternion.Lerp
            (transform.rotation, _targetRotation, Time.fixedDeltaTime * rotateSpeed);
    }

    private void Move()
    {
        _rigid.velocity = _velocity * moveSpeed;
    }

    public void SetMovement(Vector3 movement, bool isRotation = true)
    {
        _velocity = movement * Time.fixedDeltaTime;

        if (_velocity == Vector3.zero)
        {
            _enemyAnimation.ChangeAnimation(AnimationType.Idle.ToString());
        }
        else
        {
            _enemyAnimation.ChangeAnimation(AnimationType.Run.ToString());

            if (_velocity.sqrMagnitude > 0 && isRotation)
                _targetRotation = Quaternion.LookRotation(_velocity);
        }
    }

    public void StopImmediately()
    {
        _velocity = Vector3.zero;
    }
}
