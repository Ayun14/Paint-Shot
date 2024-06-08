using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Image _colorSelectPanel;
    [SerializeField] private Image _settingPanel;

    public void PlayButtonClick()
    {
        // 페이드인 처리
        SceneManager.LoadScene("Main");
    }

    public void ExitButtonClick()
    {
    }
}
