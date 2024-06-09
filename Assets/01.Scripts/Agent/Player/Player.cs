using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer; // 피부색

    [HideInInspector] public PlayerInput PlayerInput { get; private set; }
    [HideInInspector] public AgentMovement PlayerMovement { get; private set; }
    [HideInInspector] public PlayerParticleController PlayerParticleController { get; private set; }

    private CapsuleCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovement = GetComponent<AgentMovement>();
        PlayerParticleController = GetComponent<PlayerParticleController>();
    }

    private void Start()
    {
        Material mat = AgentManager.Instance.GetAgentMat();
        if (mat != null)
        {
            _renderer.material = mat;
            PlayerParticleController.PaintColorSet(mat);
            Debug.Log(mat.ToString());
        }
    }

    public void PlayerColorSet(Material mat)
    {
        _renderer.material = mat;
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
        // 총 나오는거 막아줘야함
        SetDeath();
    }
}
