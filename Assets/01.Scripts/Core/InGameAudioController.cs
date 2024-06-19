using UnityEngine;

public class InGameAudioController : Observer
{
    [Header("Countdown")]
    [SerializeField] private AudioClip _countdownClip;

    [Header("Playing")]
    [SerializeField] private AudioClip _inGameBGM;
    private AudioObject _inGameBGMObj;

    [Header("Over")]
    [SerializeField] private AudioClip _gameOverWhistle;

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

    private void Start()
    {
        AudioListener.volume = AudioManager.Instance.GetVolume();
    }

    private void OnCountdown()
    {
        if (_gameController.IsCountdown)
        {
            AudioManager.Instance.Play(_countdownClip, false);
        }
    }

    private void OnPlaying()
    {
        if (_gameController.IsPlaying)
        {
            _inGameBGMObj = AudioManager.Instance.Play(_inGameBGM, true);
        }
    }

    private void OnGameOver()
    {
        if (_gameController.IsOver)
        {
            _inGameBGMObj.DestoyAudioObject();
            AudioManager.Instance.Play(_gameOverWhistle, false);
        }
    }

    private void OnResult()
    {
        if (_gameController.IsResult)
        {

        }
    }
}
