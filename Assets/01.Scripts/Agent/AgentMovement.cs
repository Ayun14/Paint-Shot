using UnityEngine;

public class AgentMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float moveSpeed;

    private Vector3 _velocity = Vector3.zero;

    public Vector3 Velocity => _velocity;

    private Quaternion _targetRotation;

    private PlayerInput _playerInput;
    private PlayerAnimation _playerAnimation;
    private Rigidbody _rigid;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimation = transform.Find("Visual").GetComponent<PlayerAnimation>();
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _playerInput.OnMovementEvent += HandleMovementEvent;
    }

    private void OnDestroy()
    {
        _playerInput.OnMovementEvent -= HandleMovementEvent;
    }

    private void HandleMovementEvent(Vector3 vector)
    {
        SetMovement(vector);
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
        // 조이스틱을 움직이지 않는다면 바로 멈춥니다.
        if (movement == Vector3.zero)
        {
            _playerAnimation.ChangeAnimation(AnimationType.Idle.ToString());
            _playerAnimation.StopRunAnimation();
            _playerAnimation.PlayIdleAnimation();
            StopImmediately();
        }
        else
        {
            _playerAnimation.ChangeAnimation(AnimationType.Walk.ToString());
            _playerAnimation.StopRunAnimation();
            _playerAnimation.PlayRunAnimation();
            _velocity = movement * Time.fixedDeltaTime;

            if (_velocity.sqrMagnitude > 0 && isRotation)
            {
                _targetRotation = Quaternion.LookRotation(_velocity);
            }
        }
    }

    public void StopImmediately()
    {
        _velocity = Vector3.zero;
    }
}
