using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float _offsetZ;
    [SerializeField] private Transform _player;
    
    private Camera _mainCam;

    public void Init()
    {
        _mainCam = Camera.main;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (_mainCam != null)
        {
            Vector3 camPos = new Vector3
                (_player.position.x, 0f, _player.position.z + _offsetZ);
            _mainCam.transform.position = camPos;
        }
    }
}
