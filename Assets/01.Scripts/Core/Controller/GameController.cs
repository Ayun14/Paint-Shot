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

    public void ChangeGameState(GameState newGameState)
    {
        if (newGameState == _gameState) return;

        _gameState = newGameState;
        NotifyObservers();
    }
}
