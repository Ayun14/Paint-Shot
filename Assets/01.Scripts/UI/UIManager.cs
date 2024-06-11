using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Observer
{
    [Header("Countdown")]
    [SerializeField] private Image _countDownPanel;
    [SerializeField] private TextMeshProUGUI _countdownText;

    [Header("GameOver")]
    [SerializeField] private Image _ResultPanel;

    [Header("Playing")]
    [SerializeField] private Image _playerPanel;
    [SerializeField] private TextMeshProUGUI _restTimeText;
    [SerializeField] private int _playTime; // 플레이 시간
    private int _restTime; // 남은 시간

    [Header("Respawn")]
    [SerializeField] private Image _respawnImage;
    [SerializeField] private float _targetX;
    [SerializeField] private float _originX;
    [SerializeField] private TextMeshProUGUI _respawnTimeText;
    private int _currentRespawnTime; // 남은 시간

    private GameController _gameController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OpenRespawnUI();
        }
    }

    public override void Notify(Subject subject)
    {
        if (_gameController == null)
            _gameController = subject as GameController;

        if (_gameController != null)
        {
            _countDownPanel.gameObject.SetActive(_gameController.IsCountdown);
            _playerPanel.gameObject.SetActive(!_gameController.IsOver);
            _ResultPanel.gameObject.SetActive(_gameController.IsOver);

            if (_gameController.IsCountdown)
                StartCoroutine(CountdownRoutine());
            else if (_gameController.IsPlaying)
                SetRestTime();
        }
    }

    private void Start()
    {
        // 플레이 시간 초기화
        _restTime = _playTime;
        UpdateRestText();
    }

    private IEnumerator CountdownRoutine()
    {
        int countdownDuration = 3;

        for (int i = countdownDuration; i > 0; i--)
        {
            _countdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        _gameController.ChangeGameState(GameState.Playing);
    }

    private void SetRestTime() // 남은 시간
    {
        DOTween.To(() => _restTime, x => _restTime = x, 0, _playTime)
            .SetEase(Ease.Linear)
            .OnUpdate(UpdateRestText)
            .OnComplete(GameOver);
    }

    private void UpdateRestText()
    {
        int minutes = _restTime / 60;
        int seconds = _restTime % 60;
        _restTimeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        if (_restTime <= 10)
        {
            _restTimeText.color = Color.red;
            // 소리 내거나 브금 빠르게
        }
        else
            _restTimeText.color = Color.white;
    }

    private void GameOver()
    {
        _gameController.ChangeGameState(GameState.Over);
    }

    public void OpenRespawnUI()
    {
        int spawnDelayTime = 7;
        _currentRespawnTime = spawnDelayTime - 2;
        UpdateRespawnText();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_respawnImage.rectTransform
            .DOAnchorPosX(_targetX, 0.7f).SetEase(Ease.InOutSine)
            .SetUpdate(true));
        sequence.Append(DOTween.To(() => _currentRespawnTime, 
            x => _currentRespawnTime = x, 0, spawnDelayTime))
            .OnUpdate(UpdateRespawnText)
            .OnComplete(CloseRespawnUI);
    }

    private void UpdateRespawnText()
    {
        _respawnTimeText.text = $"Spawn Time...{_currentRespawnTime}";
    }

    private void CloseRespawnUI()
    {
        _respawnImage.rectTransform
            .DOAnchorPosX(_originX, 0.4f).SetEase(Ease.InSine)
            .SetUpdate(true);
    }
}