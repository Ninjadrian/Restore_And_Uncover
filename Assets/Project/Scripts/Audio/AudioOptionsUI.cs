using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        masterSlider.onValueChanged.AddListener(AudioManager.Instance.GeneralVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.MusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SFXVolume);
    }
}
