using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

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
    private float _currentRespawnTime; // 남은 시간

    [Header("Ranking")]
    [SerializeField] private Image _rankingPanel;
    [SerializeField] private Image _firstRankImage; // 1등
    [SerializeField] private TextMeshProUGUI _firstRankText;
    [SerializeField] private Image _secondRankImage; // 2등
    [SerializeField] private TextMeshProUGUI _secondRankText;
    [SerializeField] private Image _thirdRankImage; // 3등
    [SerializeField] private TextMeshProUGUI _thirdRankText;
    [SerializeField] private Image _playerRankImage; // Player
    [SerializeField] private TextMeshProUGUI _playerRankText;
    [SerializeField] private List<Material> _colorMatList = new List<Material>();

    private GameController _gameController;

    public override void Notify(Subject subject)
    {
        if (_gameController == null)
            _gameController = subject as GameController;

        if (_gameController != null)
        {
            _countDownPanel.gameObject.SetActive(_gameController.IsCountdown);
            _playerPanel.gameObject.SetActive(!_gameController.IsOver);
            _ResultPanel.gameObject.SetActive(_gameController.IsOver);
            _rankingPanel.gameObject.SetActive(!_gameController.IsOver);

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

        // Player Ranking Color Set
        foreach (Material mat in _colorMatList)
        {
            if (mat.name == $"{AgentManager.Instance.AgentColor}ParticleMat")
            {
                _playerRankImage.color = mat.color;
                break;
            }
        }
    }

    private void Update()
    {
        if (!_gameController.IsOver)
            UpdateRanking();
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
        int spawnDelayTime = 5;
        _currentRespawnTime = spawnDelayTime;
        UpdateRespawnText();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_respawnImage.rectTransform
            .DOAnchorPosX(_targetX, 0.6f).SetEase(Ease.InOutSine));
        sequence.Append(DOTween.To(() => _currentRespawnTime,
            x => _currentRespawnTime = x, 0, spawnDelayTime))
            .OnUpdate(UpdateRespawnText)
            .OnComplete(CloseRespawnUI);
    }

    private void UpdateRespawnText()
    {
        int currentTime = Mathf.CeilToInt(_currentRespawnTime);
        _respawnTimeText.text = $"Spawn Time...{currentTime.ToString()}";
    }

    private void CloseRespawnUI()
    {
        _respawnImage.rectTransform
            .DOAnchorPosX(_originX, 0.6f).SetEase(Ease.InSine)
            .SetUpdate(true);
    }

    private void UpdateRanking()
    {
        // 플레이어 3등 안에 아니면 랭크 따로 띄워주고 UI 들어갔다 나왔따 해야함
        // 적들 몇 퍼센트 먹고 있는지 알려줘야함 GroundManagerㄱ

        // 색깔 바꿔줘야함
        // 적은 Enemy_색깔 이걸로 id에서 따오기
        // GroundManager에 id 들어있는 List있음
        // _colorMatList돌면서 확인하면 됨 (mat이름 : 색깔ParticleMat)
        foreach (string s in GroundManager.Instance.idList)
        {
        //    if (s == $"Enemy_{메테이얼이르음}")
        //    {
        //        _playerRankImage.color = mat.color;
        //        break;
        //    }
        }
    }
}