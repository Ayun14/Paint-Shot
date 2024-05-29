using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootTrm;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput.OnFireEvent += PlayShootParticle;
    }

    private void OnDestroy()
    {
        _playerInput.OnFireEvent -= PlayShootParticle;
    }
    
    private void PlayShootParticle()
    {
        _shootParticle.Play();
    }
    
    private void StopShootParticle()
    {
        _shootParticle.Stop();
    }
}
