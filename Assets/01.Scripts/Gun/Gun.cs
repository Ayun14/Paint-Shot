using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootTrm;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput.OnFireEvent += PlayShootParticle;
        _playerInput.OnFireStopEvent += StopShootParticle;
    }

    private void OnDestroy()
    {
        _playerInput.OnFireEvent -= PlayShootParticle;
        _playerInput.OnFireStopEvent -= StopShootParticle;
    }
    
    private void PlayShootParticle()
    {
        _shootParticle.transform.position = _shootTrm.transform.position;
        _shootParticle.Play();
        Debug.Log("½ÇÇà");
    }
    
    private void StopShootParticle()
    {
        _shootParticle.Stop();
    }
}
