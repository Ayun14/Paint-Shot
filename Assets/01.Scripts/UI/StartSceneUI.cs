using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField]
    private List<ButtonClick> _colorButtonList
        = new List<ButtonClick>();
    [SerializeField] private Image _colorSelectPanel;
    [SerializeField] private StartScenePlayer _player;

    [Header("Sound Setting")]
    [SerializeField] private Sprite[] _soundSprites;
    [SerializeField] private Image _soundSettingImage;

    private bool _colorSelectPanelEnable = false;
    private bool _soundEnable = true;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Invoke($"Color{AgentManager.Instance.AgentColor}ButtonClick", 0.01f);

        AudioListener.volume = AudioManager.Instance.GetVolume();
        if (AudioListener.volume == 1)
        {
            _soundSettingImage.sprite = _soundSprites[1];
            _soundEnable = true;
        }
        else
        {
            _soundSettingImage.sprite = _soundSprites[0];
            _soundEnable = false;
        }
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void ColorSelectButtonClick()
    {
        _colorSelectPanelEnable = !_colorSelectPanelEnable;
        _colorSelectPanel.gameObject.SetActive(_colorSelectPanelEnable);
    }

    public void SoundSettingButtonClick()
    {
        _soundEnable = !_soundEnable;
        if (_soundEnable)
            _soundSettingImage.sprite = _soundSprites[1];
        else
            _soundSettingImage.sprite = _soundSprites[0];

        AudioManager.Instance.VolumeChange();
    }

    #region Color 버튼 관련
    private void CheckedButtonOff()
    {
        foreach (ButtonClick button in _colorButtonList)
            button.OffCheckImage();
    }

    public void ColorRedButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Red);
        _player.ChangePlayerMat(AgentColor.Red);
    }

    public void ColorOrangeButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Orange);
        _player.ChangePlayerMat(AgentColor.Orange);
    }

    public void ColorYellowButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Yellow);
        _player.ChangePlayerMat(AgentColor.Yellow);
    }

    public void ColorGreenButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Green);
        _player.ChangePlayerMat(AgentColor.Green);
    }

    public void ColorBlueButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Blue);
        _player.ChangePlayerMat(AgentColor.Blue);
    }

    public void ColorMintButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Mint);
        _player.ChangePlayerMat(AgentColor.Mint);
    }

    public void ColorPinkButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Pink);
        _player.ChangePlayerMat(AgentColor.Pink);
    }

    public void ColorPurpleButtonClick()
    {
        CheckedButtonOff();
        AgentManager.Instance.ChangePlayerColor(AgentColor.Purple);
        _player.ChangePlayerMat(AgentColor.Purple);
    }
    #endregion
}
