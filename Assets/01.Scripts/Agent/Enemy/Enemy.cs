using UnityEngine;

public enum EnemyState
{
    Idle, Paint, Attack
}

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine<EnemyState> StateMachine { get; private set; }

    #region Components
    [HideInInspector] public AgentAnimation EnemyAnimation { get; private set; }
    #endregion

    public float moveSpeed;
    private float _defaultMoveSpeed;
    public bool isActive;

    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private LayerMask _whatIsObstacle;
    [SerializeField] private LayerMask _whatIsNode;

    [Header("Attack Settings")]
    public float runAwayDistance; //어그로가 완전히 빠지는 거리
    public float attackDistance;
    [HideInInspector] public Transform targetTrm;
    [HideInInspector] public CapsuleCollider colliderCompo;

    [Header("Paint Settings")]
    public float paintDistance; // 이만큼 영역이 다 칠해져 있는지
    public float paintCheckDistance; // 얼마나 앞에서 체크할건지

    private void Awake()
    {
        _defaultMoveSpeed = moveSpeed;
        EnemyAnimation = transform.Find("Visual").GetComponent<AgentAnimation>();
        colliderCompo = GetComponent<CapsuleCollider>();

        // State 세팅
        StateMachine = new EnemyStateMachine<EnemyState>();
        StateMachine.AddState(EnemyState.Idle,
            new EnemyIdleState(this, StateMachine, EnemyState.Idle.ToString()));
        StateMachine.AddState(EnemyState.Paint,
            new EnemyPaintState(this, StateMachine, EnemyState.Paint.ToString()));
        StateMachine.AddState(EnemyState.Attack,
            new EnemyAttackState(this, StateMachine, EnemyState.Attack.ToString()));
    }

    private void Start()
    {
        StateMachine.Initialize(EnemyState.Idle, this);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    // 플레이어가 도망갔는지
    public Collider IsPlayerDetected()
    {
        bool isHit = Physics.SphereCast(transform.position,
            runAwayDistance, Vector3.up, out RaycastHit hit, 0, _whatIsPlayer);
        return isHit ? hit.collider : null;
    }

    // 사이에 장애물이 있는지
    public bool IsObstacleInLine(float distance, Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, distance, _whatIsObstacle);
    }

    // 주변에 칠할 땅이 있는지
    public bool IsCanPaint()
    {
        RaycastHit[] colliders = Physics.SphereCastAll(transform.position, 
            paintDistance, Vector3.forward, paintCheckDistance, _whatIsNode);
        foreach (RaycastHit collider in colliders)
        {
            if (collider.transform.TryGetComponent(out GroundNode node))
                if (node.nodeId != transform.name) return false;
        }
        return true;
    }

    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, runAwayDistance); //적 감지거리
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.white;
    }
}
