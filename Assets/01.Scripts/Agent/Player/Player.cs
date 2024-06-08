using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CapsuleCollider _collider;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void SetDeath()
    {
        _playerInput.SetPlayerInput(false);
        _collider.enabled = false;
    }
}
