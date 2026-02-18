using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    //public AudioSource sfxSource;

    public AudioCollectionSO musicCollection;
    //public AudioCollectionSO sfxCollection;
    public AudioMixer audioMixer;

    public static AudioManager Instance;

    private int intensityIndex;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(musicSource.gameObject);
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = musicCollection.audioClips[intensityIndex];
        musicSource.Play();
        if (intensityIndex < musicCollection.audioClips.Length - 1)
        {
            intensityIndex++;
        }
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
