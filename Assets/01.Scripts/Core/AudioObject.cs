using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private float pitchRandomness = 0.2f;
    private float basePitch;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip, bool isLooping)
    {
        StartCoroutine(PlayClipCor(clip, isLooping));
    }

    private IEnumerator PlayClipCor(AudioClip clip, bool isLooping)
    {
        if (isLooping)
            audioSource.loop = true;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);

        if (!isLooping) Destroy(gameObject);
    }

    public void PlayClipwithVariablePitch(AudioClip clip, bool isLooping)
    {
        float randomPitcch = Random.Range(-pitchRandomness, +pitchRandomness);
        audioSource.pitch = basePitch + randomPitcch;
        PlayClip(clip, isLooping);
    }

    public void DestoyAudioObject()
    {
        Destroy(gameObject);
    }
}
