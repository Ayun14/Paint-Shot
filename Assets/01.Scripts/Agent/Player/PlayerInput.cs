using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> MovementEvent;
    public Vector3 MousePosition { get; private set; }

    private bool _playerInputEnabled = true;

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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);

        MovementEvent?.Invoke(moveVector);
    }
}
