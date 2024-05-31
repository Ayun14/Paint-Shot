using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementEvent;
    public event Action OnFireEvent;
    public event Action OnFireStopEvent;

    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Transform _fireButtonTrm;

    private bool _playerInputEnabled = true;

    private void Start()
    {
        EventTrigger trigger = _fireButtonTrm.GetComponent<EventTrigger>();

        // PointerDown 이벤트 추가
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) =>
        {
            if (!_playerInputEnabled) return;
            OnFireEvent?.Invoke();
        });
        trigger.triggers.Add(pointerDownEntry);

        // PointerUp 이벤트 추가
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) =>
        {
            if (!_playerInputEnabled) return;
            OnFireStopEvent?.Invoke();
        });
        trigger.triggers.Add(pointerUpEntry);
    }

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
}
