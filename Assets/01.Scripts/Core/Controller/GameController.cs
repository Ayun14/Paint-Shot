using System.Diagnostics;
using UnityEngine;

public enum GameState
{
    Countdown, Playing, Over
}

public class GameController : Subject
{
    private GameState _gameState;

    public bool IsCountdown => _gameState == GameState.Countdown;
    public bool IsPlaying => _gameState == GameState.Playing;
    public bool IsOver => _gameState == GameState.Over;

    private CameraController _cameraController;
    private UIManager _uiManager;
    private AgentController _agentController;

    private void Awake()
    {
        _cameraController = (CameraController)FindObjectOfType(typeof(CameraController));
        _uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        _agentController = (AgentController)FindObjectOfType(typeof(AgentController));

        if (_cameraController)
            Attach(_cameraController);
        if (_uiManager)
            Attach(_uiManager);
        if (_agentController)
            Attach(_agentController);

        _gameState = GameState.Countdown;
        NotifyObservers();
    }

    private void OnDestroy()
    {
        if (_cameraController)
            Detach(_cameraController);
        if (_uiManager)
            Detach(_uiManager);
        if (_agentController)
            Detach(_agentController);
    }

    public void ChangeGameState(GameState newGameState)
    {
        if (newGameState == _gameState) return;

        _gameState = newGameState;
        NotifyObservers();
    }
}
