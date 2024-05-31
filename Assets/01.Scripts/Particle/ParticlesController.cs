using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private float _minRadius = 0.1f;
    [SerializeField] private float _maxRadius = 0.3f;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _hardness = 1;
    [SerializeField] private Color _paintColor;

    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> _collisionEventList;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _collisionEventList = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (_particleSystem == null) return;

        Debug.Log(other.name);

        int numCollisionEvents = 
            _particleSystem.GetCollisionEvents(other, _collisionEventList);

        Paintable p = other.GetComponent<Paintable>();

        if (p != null)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = _collisionEventList[i].intersection;
                float radius = Random.Range(_minRadius, _maxRadius);
                PaintManager.Instance.paint
                    (p, pos, radius, _hardness, _strength, _paintColor);
            }
        }
    }
}
