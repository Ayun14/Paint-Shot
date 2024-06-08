using UnityEngine;

public enum GameState
{
    Countdown, Playing, Over
}

public class GameController : Subject
{
    private GameState _gameState = GameState.Countdown;

    public bool IsCountdown => _gameState == GameState.Countdown;
    public bool IsPlaying => _gameState == GameState.Playing;
    public bool IsOver => _gameState == GameState.Over;

    private CameraController _cameraController;
    private UIManager _uiManager;

    private void Awake()
    {
        _cameraController = (CameraController)FindObjectOfType(typeof(CameraController));
        _uiManager = (UIManager)FindObjectOfType(typeof(UIManager));

        if (_cameraController)
            Attach(_cameraController);
        if (_uiManager)
            Attach(_uiManager);
    }

    private void OnDestroy()
    {
        if (_cameraController)
            Detach(_cameraController);
        if (_uiManager)
            Detach(_uiManager);
    }

    private void Start()
    {
        NotifyObservers();
    }

    public void ChangeGameState(GameState newGameState)
    {
        if (newGameState == _gameState) return;

        _gameState = newGameState;
        NotifyObservers();
    }
}
