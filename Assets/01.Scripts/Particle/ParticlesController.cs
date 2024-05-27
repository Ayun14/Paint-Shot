using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private float _minRadius = 0.05f;
    [SerializeField] private float _maxRadius = 0.2f;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _hardness = 1;
    [SerializeField] private Color _paintColor;

    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> _collisionEventList;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _particleSystem.GetCollisionEvents(other, _collisionEventList);

        //Paintable p = other.GetComponent<Paintable>();
        //if (p != null)
        //{
        //    for (int i = 0; i < numCollisionEvents; i++)
        //    {
        //        Vector3 pos = collisionEvents[i].intersection;
        //        float radius = Random.Range(minRadius, maxRadius);
        //        PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
        //    }
        //}
    }
}
