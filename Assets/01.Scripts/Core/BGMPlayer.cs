using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _bgmClip;

    private void Start()
    {
        AudioManager.Instance.Play(_bgmClip, true);
    }
}
