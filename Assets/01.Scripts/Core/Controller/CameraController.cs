using Cinemachine;
using UnityEngine;

public class CameraController : Observer
{
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [SerializeField] private CinemachineVirtualCamera _topViewCam;

    private GameController _subject;

    public override void Notify(Subject subject)
    {
        if (_subject == null)
            _subject = subject as GameController;

        if (_subject != null)
            CameraChangeCheck();
    }

    private void CameraChangeCheck()
    {
        if (_subject.IsOver) // 게임 결과 띄워주는 Top View Camrea 보여주기
        {
            _followCam.Priority = 0;
            _topViewCam.Priority = 1;
        }
        else // Follow Camera 보여주기
        {
            _topViewCam.Priority = 0;
            _followCam.Priority = 1;
        }
    }
}
