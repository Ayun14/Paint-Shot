using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Transform _audioObj;

    private bool isMuted = false;
    public bool IsMuted => isMuted;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Application.targetFrameRate = 60;

        if (PlayerPrefs.GetInt("Volume") == 0)
            isMuted = true;
        else
            isMuted = false;

        VolumeSetting();
    }

    public AudioObject Play(AudioClip clip, bool isLoopSound, bool isWithVariablePitch = false)
    {
        Transform audio = Instantiate(_audioObj, transform.position, Quaternion.identity);
        AudioObject audioObj = audio.GetComponent<AudioObject>();

        if (isWithVariablePitch)
            audioObj.PlayClipwithVariablePitch(clip, isLoopSound);
        else
            audioObj.PlayClip(clip, isLoopSound);

        return audioObj;
    }

    public void VolumeChange()
    {
        isMuted = !isMuted;
        VolumeSetting();
    }

    private void VolumeSetting()
    {
        if (isMuted)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Volume", 0);
        }
        else
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Volume", 1);
        }
    }

    public int GetVolume()
    {
        return PlayerPrefs.GetInt("Volume");
    }
}
