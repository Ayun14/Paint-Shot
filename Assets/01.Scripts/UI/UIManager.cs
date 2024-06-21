using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Observer
{
    [Header("Countdown")]
    [SerializeField] private Image _countDownPanel;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private AudioClip _gameStartClip;

    [Header("GameOver")]
    [SerializeField] private AudioClip _timerClip;
    private AudioObject _timerObject;

    [SerializeField] private Image _ResultPanel;
    [SerializeField] private Image _homeButton;
    [SerializeField] private float _homeButtonTargetX;

    [SerializeField]
    private List<Image> _resultRankImageList
        = new List<Image>();  // 왼쪽에서 순서대로 등장할 이미지
    [SerializeField] private float _resultTargetX;

    [SerializeField]
    private List<Image> _characterImageList
        = new List<Image>();
    [SerializeField]
    private List<Sprite> _characterColorSprites
        = new List<Sprite>();
    [SerializeField]
    private List<TextMeshProUGUI> _persentTextList
        = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> _nameTextList
        = new List<TextMeshProUGUI>();

    [Header("Playing")]
    [SerializeField] private Image _playerPanel;
    [SerializeField] private TextMeshProUGUI _restTimeText;
    [SerializeField] private int _playTime; // 플레이 시간
    private int _restTime; // 남은 시간

    [Header("Respawn")]
    [SerializeField] private Image _respawnImage;
    [SerializeField] private float _respawnTargetX;
    [SerializeField] private float _respawnOriginX;
    [SerializeField] private TextMeshProUGUI _respawnTimeText;
    private int _spawnDelayTime = 3; // 스폰 시간

    [SerializeField] private AudioClip _uiOpenClip;

    [SerializeField] private Image _rankingPanel;
    [SerializeField] private List<Image> _rankImageList = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> _rankPersentTextList = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> _rankNameTextList = new List<TextMeshProUGUI>();

    [SerializeField] private Image _playerRankImage; // Player
    [SerializeField] private TextMeshProUGUI _playerRankText;
    [SerializeField] private float _rankTargetX;
    [SerializeField] private float _rankOriginX;
    private Color _playerRankColor;
    private bool _isPlayerRankIn = false;

    [SerializeField] private List<Material> _colorMatList = new List<Material>();

    private Dictionary<string, float> _rankColorDictionary; // 순위를 기억하는 리스트
    private GameController _gameController;

    public override void Notify(Subject subject)
    {
        if (_gameController == null)
            _gameController = subject as GameController;

        if (_gameController != null)
        {
            _countDownPanel.gameObject.SetActive(_gameController.IsCountdown);
            _ResultPanel.gameObject.SetActive(_gameController.IsResult);
            _rankingPanel.gameObject.SetActive(_gameController.IsPlaying);
            if (_gameController.IsCountdown || _gameController.IsPlaying)
                _playerPanel.gameObject.SetActive(true);
            else
                _playerPanel.gameObject.SetActive(false);


            if (_gameController.IsCountdown)
                StartCoroutine(CountdownRoutine());
            else if (_gameController.IsPlaying)
                SetRestTime();
        }
    }

    private void Start()
    {
        _rankColorDictionary = new Dictionary<string, float>();

        // 플레이 시간 초기화
        _restTime = _playTime;
        UpdateRestText();

        // Player Ranking Color Set
        foreach (Material mat in _colorMatList)
        {
            if (mat.name == AgentManager.Instance.AgentColor.ToString())
            {
                _playerRankImage.color = mat.color;
                _playerRankColor = mat.color;
                break;
            }
        }
    }

    private void LateUpdate()
    {
        if (_gameController.IsPlaying)
            UpdateRanking();
    }

    public void HomeButtonClick()
    {
        StopAllCoroutines();
        GroundManager.Instance.ResetGroundManager();

        SceneManager.LoadScene(2);
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
        AudioManager.Instance.Play(_gameStartClip, false);
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
            if (_timerObject == null)
                _timerObject = AudioManager.Instance.Play(_timerClip, true);
        }
        else
            _restTimeText.color = Color.white;
    }

    private void GameOver()
    {
        Destroy(_timerObject.gameObject);
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        _gameController.ChangeGameState(GameState.Over);
        yield return new WaitForSeconds(3f);
        StartCoroutine(ResultRoutine());
    }

    private IEnumerator ResultRoutine()
    {
        _gameController.ChangeGameState(GameState.Result);
        yield return new WaitForSeconds(3.5f);

        int index = 0;
        foreach (var ranking in _rankColorDictionary)
        {
            // 이미지 바꾸기
            foreach (Sprite sprite in _characterColorSprites)
            {
                // Enemy
                if (ranking.Key == $"Enemy_{sprite.name}(Clone)")
                    _characterImageList[index].sprite = sprite;

                // Player
                if (ranking.Key == "Player")
                {
                    if (AgentManager.Instance.AgentColor.ToString() == sprite.name)
                        _characterImageList[index].sprite = sprite;
                    _nameTextList[index].text = "Player";
                }
            }

            foreach (var name in GroundManager.Instance.nameList)
            {
                if ($"Enemy_{name.Value}" == $"{ranking.Key}")
                    _nameTextList[index].text = name.Key;
            }

            // 몇 퍼센트 인지
            string result = ranking.Value.ToString("F2");
            _persentTextList[index].text = $"{result}%";

            index++;
        }

        foreach (Image image in _resultRankImageList)
        {
            image.rectTransform
            .DOAnchorPosX(_resultTargetX, 1f).SetEase(Ease.InOutSine);
            AudioManager.Instance.Play(_uiOpenClip, false);
            yield return new WaitForSeconds(0.25f);
        }
        AudioManager.Instance.Play(_uiOpenClip, false);

        yield return new WaitForSeconds(0.5f);
        _homeButton.rectTransform
            .DOAnchorPosX(_homeButtonTargetX, 1f).SetEase(Ease.InOutSine);
    }

    public void OpenRespawnUI()
    {
        _respawnTimeText.text = $"Spawn Time...{_spawnDelayTime}";

        _respawnImage.rectTransform
            .DOAnchorPosX(_respawnTargetX, 0.6f).SetEase(Ease.InOutSine);
        StartCoroutine(SpawnCountdownRoutine());
    }

    private IEnumerator SpawnCountdownRoutine()
    {
        for (int i = _spawnDelayTime; i >= 0; --i)
        {
            _respawnTimeText.text = $"Spawn Time...{i}";

            if (i == 0)
                break;

            yield return new WaitForSeconds(1f);
        }

        CloseRespawnUI();
    }

    private void CloseRespawnUI()
    {
        _respawnImage.rectTransform
            .DOAnchorPosX(_respawnOriginX, 0.6f).SetEase(Ease.InSine);
    }

    #region Ranking System
    private void UpdateRanking()
    {
        _rankColorDictionary = GroundManager.Instance.GroundRanking();

        int rank = 0;
        int playerRank = 0;
        foreach (var entry in _rankColorDictionary)
        {
            if (rank < 3)
            {
                foreach (Material mat in _colorMatList)
                {
                    if (entry.Key.ToString()
                        == $"Enemy_{mat.name}(Clone)")
                    {
                        _rankImageList[rank].color = mat.color;
                        string result = entry.Value.ToString("F2");
                        _rankPersentTextList[rank].text = $"{rank + 1}  -  {result}%";

                        foreach (var name in GroundManager.Instance.nameList)
                        {
                            if ($"{name.Value}" == $"{mat.name}(Clone)")
                                _rankNameTextList[rank].text = name.Key.ToString();
                        }
                        break;
                    }
                }
            }

            if (entry.Key == "Player")
                playerRank = rank;

            rank++;
        }

        // Player
        if (playerRank < 3)
        {
            if (!_isPlayerRankIn)
            {
                _isPlayerRankIn = true;
                _playerRankImage.rectTransform
                    .DOAnchorPosX(_rankOriginX, 0.6f)
                    .SetEase(Ease.InSine); // out
            }

            _rankImageList[playerRank].color = _playerRankColor;
            string result = _rankColorDictionary["Player"].ToString("F2");
            _rankPersentTextList[playerRank].text =
                $"{playerRank + 1}  -  {result}%";
            _rankNameTextList[playerRank].text = "Player";
        }
        else
        {
            if (_isPlayerRankIn)
            {
                _isPlayerRankIn = false;
                _playerRankImage.rectTransform
                    .DOAnchorPosX(_rankTargetX, 0.6f)
                    .SetEase(Ease.InOutSine); // in
            }

            string result = _rankColorDictionary["Player"].ToString("F2");
            _playerRankText.text = $"{playerRank + 1}  -  {result}%  Player";
        }
    }
    #endregion
}