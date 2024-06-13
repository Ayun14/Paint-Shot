using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer; // 피부색
    [HideInInspector] public PlayerInput PlayerInput { get; private set; }
    [HideInInspector] public AgentMovement PlayerMovement { get; private set; }
    [HideInInspector] public PlayerParticleController PlayerParticleController { get; private set; }
    [HideInInspector] public Health PlayerHealth { get; private set; }
    [HideInInspector] public PlayerAnimation PlayerAnimation { get; private set; }
    public AgentGun AgentGun;
    private CapsuleCollider _collider;

    private Vector3 _spawnPos; // 스폰, 리스폰되는 장소

    private float _spawnDelayTime = 5f;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerMovement = GetComponent<AgentMovement>();
        PlayerHealth = GetComponent<Health>();
        PlayerAnimation = transform.Find("Visual").GetComponent<PlayerAnimation>();
        PlayerParticleController = GetComponent<PlayerParticleController>();
    }

    private void Start()
    {
        _spawnPos = transform.position;

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

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(_spawnDelayTime);

        PlayerAnimation.ChangeAnimation(AnimationType.Idle.ToString());
        PlayerInput.SetPlayerInput(true);
        _collider.enabled = true;
        PlayerHealth.HealthReset();

        transform.position = _spawnPos;
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
