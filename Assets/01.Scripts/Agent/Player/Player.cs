using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private CapsuleCollider _collider;
    public PlayerInput PlayerInput { get; private set; }
    public AgentMovement PlayerMovement { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovement = GetComponent<AgentMovement>();
    }

    public void SetRevival() // 부활 설정
    {
        PlayerInput.SetPlayerInput(true);
        _collider.enabled = true;
    }

    public void SetDeath()
    {
        PlayerInput.SetPlayerInput(false);
        PlayerMovement.StopImmediately();
        _collider.enabled = false;
    }

    public void SetGameOver()
    {
        PlayerMovement.StopImmediately();
    }
}
