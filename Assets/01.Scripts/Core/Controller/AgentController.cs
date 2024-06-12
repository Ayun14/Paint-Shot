using UnityEngine;

public class AgentController : Observer
{
    [SerializeField] private Player _player;

    private GameController _gameController;

    public override void Notify(Subject subject)
    {
        if (_gameController == null)
            _gameController = subject as GameController;

        if (_gameController != null)
        {
            OnCountdown();
            OnPlaying();
            OnGameOver();
        }
    }

    private void OnCountdown()
    {
        if (_gameController.IsCountdown)
        {
            AgentManager.Instance.AgentSpawn();
            _player.transform.position = AgentManager.Instance.AgentSpawnPos;
            _player.transform.rotation = AgentManager.Instance.AgentRotation;
        }
    }

    private void OnPlaying()
    {
        if (_gameController.IsPlaying)
        {
            AgentManager.Instance.GameStart();
        }
    }

    private void OnGameOver()
    {
        if (_gameController.IsOver)
        {
            _player.SetGameOver();
            AgentManager.Instance.GameOver();
        }
    }
}
