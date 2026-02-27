using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    public AudioCollectionSO musicCollection;
    public AudioMixer audioMixer;

    public static AudioManager Instance;

    private int intensityIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(musicSource.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayCurrentTrack();
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
            PlayNextTrack();
    }

    private void PlayCurrentTrack()
    {
        musicSource.clip = musicCollection.audioClips[intensityIndex];
        musicSource.Play();
    }

    private void PlayNextTrack()
    {
        intensityIndex++;

        if (intensityIndex >= musicCollection.audioClips.Length)
        {
            intensityIndex = 0;
        }
        PlayCurrentTrack();
    }

    public void MusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void GeneralVolume(float volume)
    {
        audioMixer.SetFloat("GeneralVolume", volume);
    }

}
