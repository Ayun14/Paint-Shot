using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [Header("Paint Setting")]
    [SerializeField] private float _minRadius = 0.1f;
    [SerializeField] private float _maxRadius = 0.3f;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _hardness = 1;
    public Color paintColor;

    [Header("Attack Setting")]
    [SerializeField] private int _damage = 1;

    [Header("Id Setting")]
    [SerializeField] private Transform _owner;
    private string _name;

    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> _collisionEventList;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _collisionEventList = new List<ParticleCollisionEvent>();
    }

    void Start()
    {
        string color = _owner.name.Substring(6);
        _name = AgentManager.Instance.GetRandomNickName();
        GroundManager.Instance.AddIdList(_owner.name, _name, color);
    }

    void OnParticleCollision(GameObject other)
    {
        if (_particleSystem == null) return;

        int numCollisionEvents =
            _particleSystem.GetCollisionEvents(other, _collisionEventList);

        if (other.TryGetComponent(out Paintable p))
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = _collisionEventList[i].intersection;
                float radius = Random.Range(_minRadius, _maxRadius);

                // 물감 그리기
                PaintManager.Instance.paint
                    (p, pos, radius, _hardness, _strength, paintColor);

                // 누구 땅인지
                GroundManager.Instance.GroundPainted(pos, radius, _owner.name);
            }
        }

        if (other.TryGetComponent(out IDamageable health))
            health.ApplyDamage(_damage);
    }
}
