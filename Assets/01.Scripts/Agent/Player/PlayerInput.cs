using System;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> MovementEvent;

    [SerializeField] private FixedJoystick _joystick;

    private bool _playerInputEnabled = false;

    public Vector3 MousePosition { get; private set; }


    public void SetPlayerInput(bool enable)
    {
        _playerInputEnabled = enable;
    }

    private void Update()
    {
        if (_playerInputEnabled) return;

        GetMovementInput();
    }

    private void GetMovementInput()
    {
        float xInput = _joystick.Horizontal;
        float zInput = _joystick.Vertical;

        Vector3 moveInput = new Vector3(xInput, 0, zInput);

        MovementEvent?.Invoke(moveInput.normalized);
    }
}
