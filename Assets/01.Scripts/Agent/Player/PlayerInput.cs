using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementEvent;
    public event Action OnFireEvent;

    [SerializeField] private FixedJoystick _joystick;

    private bool _playerInputEnabled = true;

    public void SetPlayerInput(bool enable)
    {
        _playerInputEnabled = enable;
    }

    private void Update()
    {
        if (!_playerInputEnabled) return;

        GetMovementInput();
    }

    private void GetMovementInput()
    {
        float xInput = _joystick.Horizontal;
        float zInput = _joystick.Vertical;

        Vector3 moveInput = new Vector3(xInput, 0, zInput);

        OnMovementEvent?.Invoke(moveInput.normalized);
    }

    public void FireButtonClick()
    {
        if (!_playerInputEnabled) return;

        OnFireEvent?.Invoke();
    }
}
