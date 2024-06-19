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
            OnResult();
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
            AgentManager.Instance.EnemyGameStart();
        }
    }

    private void OnGameOver()
    {
        if (_gameController.IsOver)
        {
            _player.SetGameOver();
            AgentManager.Instance.EnemyGameOver();
        }
    }

    private void OnResult()
    {
        if (_gameController.IsResult)
        {
            _player.SetResult();
            AgentManager.Instance.EnemyGameOver();
            AgentManager.Instance.ResetAgentManager();
        }
    }
}
