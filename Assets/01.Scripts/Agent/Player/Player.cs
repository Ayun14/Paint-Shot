using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _renderer; // 피부색
    [SerializeField] private AudioClip _playerDeadClip;

    [HideInInspector] public PlayerInput PlayerInput { get; private set; }
    [HideInInspector] public AgentMovement PlayerMovement { get; private set; }
    [HideInInspector] public PlayerParticleController PlayerParticleController { get; private set; }
    [HideInInspector] public Health PlayerHealth { get; private set; }
    [HideInInspector] public PlayerAnimation PlayerAnimation { get; private set; }
    public AgentGun AgentGun;
    private CapsuleCollider _collider;

    private Vector3 _spawnPos; // 스폰, 리스폰되는 장소

    private float _spawnDelayTime = 3f;

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
        gameObject.SetActive(true);
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
        AgentGun.isAgentActive = false;
        yield return new WaitForSeconds(_spawnDelayTime);
        AgentGun.isAgentActive = true;

        gameObject.SetActive(false);
        PlayerAnimation.ChangeAnimation(AnimationType.Idle.ToString());
        PlayerInput.SetPlayerInput(true);
        _collider.enabled = true;
        PlayerHealth.HealthReset();
        AgentGun.OnDeadPaintFill();
        gameObject.SetActive(true);

        transform.position = _spawnPos;
    }

    public void SetDeath()
    {
        AgentGun.StopPaintParticle();
        PlayerInput.SetPlayerInput(false);
        PlayerMovement.StopImmediately();
        _collider.enabled = false;

        PlayerAnimation.StopPaintAnimation();
        PlayerAnimation.StopRunAnimation();
        PlayerAnimation.StopIdleAnimation();
    }

    public void SetResult()
    {
        gameObject.SetActive(false);
    }

    public void SetGameOver()
    {
        StopAllCoroutines();

        AgentGun.isAgentActive = false;
        AgentGun.StopPaintParticle();
        PlayerAnimation.ChangeAnimation(AnimationType.Idle.ToString());
        SetDeath();
    }

    public void DeadSoundPlay()
    {
        AudioManager.Instance.Play(_playerDeadClip, false);
    }
}
