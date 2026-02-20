using UnityEngine;
using UnityEngine.UI;

public class VideoOptionsUI : MonoBehaviour
{
    public Slider BrightnessSlider;

    private void Start()
    {
        BrightnessSlider.onValueChanged.RemoveAllListeners();

        BrightnessSlider.onValueChanged.AddListener(AudioManager.Instance.GeneralVolume);
    }
}
