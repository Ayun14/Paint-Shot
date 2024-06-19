public class PlayerAnimation : AgentAnimation
{
    private PlayerInput _playerInput;

    protected override void Awake()
    {
        base.Awake();

        _playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Start()
    {
        _playerInput.OnFireEvent += PlayPaintAnimation;
        _playerInput.OnFireStopEvent += StopPaintAnimation;
    }

    private void OnDestroy()
    {
        _playerInput.OnFireEvent -= PlayPaintAnimation;
        _playerInput.OnFireStopEvent -= StopPaintAnimation;
    }
}
