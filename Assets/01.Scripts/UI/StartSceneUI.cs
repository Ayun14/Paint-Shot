using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private List<ButtonClick> _colorButtonList 
        = new List<ButtonClick>();
    
    [SerializeField] private Image _colorSelectPanel;
    [SerializeField] private Image _settingPanel;

    [SerializeField] private StartScenePlayer _player;

    private bool _colorSelectPanelEnable = false;
    private bool _settingPanelEnable = false;

    private void Start()
    {
        // 실행 왜 안디..
        SendMessage($"Color{AgentManager.Instance.AgentColor}ButtonClick");
    }

    public void PlayButtonClick()
    {
        // 페이드인 처리
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

    public void SettingButtonClick()
    {
        _settingPanelEnable = !_settingPanelEnable;
        _settingPanel.gameObject.SetActive(_settingPanelEnable);
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
