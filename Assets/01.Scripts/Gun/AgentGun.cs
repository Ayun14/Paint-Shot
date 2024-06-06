using UnityEngine;

public class AgentGun : Gun
{
    [SerializeField] private PlayerInput _playerInput;

    protected override void Start()
    {
        base.Start();

        _playerInput.OnFireEvent += PlayPaintParticle;
        _playerInput.OnFireStopEvent += StotPaintParticle;
    }

    private void OnDestroy()
    {
        _playerInput.OnFireEvent -= PlayPaintParticle;
        _playerInput.OnFireStopEvent -= StotPaintParticle;
    }
}
