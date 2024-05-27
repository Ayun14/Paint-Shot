using System;
using UnityEngine;
using Input = UnityEngine.Input;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementEvent;
    public event Action OnFireEvent;

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
        GetFireInput();
    }

    private void GetMovementInput()
    {
        float xInput = _joystick.Horizontal;
        float zInput = _joystick.Vertical;

        Vector3 moveInput = new Vector3(xInput, 0, zInput);

        OnMovementEvent?.Invoke(moveInput.normalized);
    }

    private void GetFireInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnFireEvent?.Invoke();
        }
    }
}
