public enum GameState
{
    Countdown, Playing, Over, Result
}

public class GameController : Subject
{
    private GameState _gameState;

    public bool IsCountdown => _gameState == GameState.Countdown;
    public bool IsPlaying => _gameState == GameState.Playing;
    public bool IsOver => _gameState == GameState.Over;
    public bool IsResult => _gameState == GameState.Result;

    private CameraController _cameraController;
    private UIManager _uiManager;
    private AgentController _agentController;
    private InGameAudioController _audioController;

    private void Awake()
    {
        _cameraController = (CameraController)FindObjectOfType(typeof(CameraController));
        _uiManager = (UIManager)FindObjectOfType(typeof(UIManager));
        _agentController = (AgentController)FindObjectOfType(typeof(AgentController));
        _audioController = (InGameAudioController)FindObjectOfType(typeof(InGameAudioController));

        if (_cameraController)
            Attach(_cameraController);
        if (_uiManager)
            Attach(_uiManager);
        if (_agentController)
            Attach(_agentController);
        if (_audioController)
            Attach(_audioController);

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
        if (_audioController)
            Detach(_audioController);
    }

    public void ChangeGameState(GameState newGameState)
    {
        if (newGameState == _gameState) return;

        _gameState = newGameState;
        NotifyObservers();
    }
}
