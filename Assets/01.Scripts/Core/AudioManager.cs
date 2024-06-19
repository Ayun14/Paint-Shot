using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private Transform _audioObj;

    private bool isMuted = false;
    public bool IsMuted => isMuted;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
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
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }

    public int GetVolume()
    {
        if (isMuted)
            return 0;

        return 1;
    }
}
