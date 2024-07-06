using UnityEngine;

public class InGameAudioController : Observer
{
    [Header("Countdown")]
    [SerializeField] private AudioClip _countdownClip;

    [Header("Playing")]
    [SerializeField] private AudioClip _inGameBGM;
    private AudioObject _inGameBGMObj;

    [Header("Over")]
    [SerializeField] private AudioClip _gameOverWhistleClip;

    [Header("Result")]
    [SerializeField] private AudioClip _drumRollClip;

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
            OnReault();
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
            AudioManager.Instance.Play(_gameOverWhistleClip, false);
        }
    }

    private void OnReault()
    {
        if (_gameController.IsResult)
            Invoke("DrumRollSound", 2);
    }

    private void DrumRollSound()
    {
        AudioManager.Instance.Play(_drumRollClip, false);
    }
}
