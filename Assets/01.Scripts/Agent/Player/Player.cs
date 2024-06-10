using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer; // 피부색
    [HideInInspector] public PlayerInput PlayerInput { get; private set; }
    [HideInInspector] public AgentMovement PlayerMovement { get; private set; }
    [HideInInspector] public PlayerParticleController PlayerParticleController { get; private set; }
    public AgentGun AgentGun;
    private CapsuleCollider _collider;

    [HideInInspector] public Vector3 spawnPos; // 스폰, 리스폰되는 장소

    private float _spawnDelayTime = 3f;

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
        }
    }

    public void PlayerColorSet(Material mat)
    {
        _renderer.material = mat;
    }

    public void Respawn(bool isInit = false) // 처음 스폰이면 isInit = ture
    {
        if (isInit)
            StartCoroutine(RespawnRoutine(0));
        else
            StartCoroutine(RespawnRoutine(_spawnDelayTime));
    }

    private IEnumerator RespawnRoutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        transform.position = spawnPos;
        PlayerInput.SetPlayerInput(true);
        _collider.enabled = true;
    }

    public void SetDeath()
    {
        AgentGun.StopPaintParticle();
        PlayerInput.SetPlayerInput(false);
        PlayerMovement.StopImmediately();
        _collider.enabled = false;
    }

    public void SetGameOver()
    {
        AgentGun.StopPaintParticle();
        SetDeath();
    }
}
