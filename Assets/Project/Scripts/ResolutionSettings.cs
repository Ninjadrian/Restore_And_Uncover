using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResolutionSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private List<Resolution> filtered = new List<Resolution>();

    private void Start()
    {
        resolutions = Screen.resolutions;

        filtered.Clear();
        var seen = new HashSet<string>();

        foreach (var r in resolutions)
        {
            string key = $"{r.width}x{r.height}";
            if (seen.Add(key))
            {
                filtered.Add(r);
            }
        }

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < filtered.Count; i++) {
            var r = filtered[i];
            options.Add($"{r.width} x {r.height}");

            if (r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height)
            {
            currentIndex = i; 
            } 
        }

        resolutionDropdown.AddOptions(options);

        int savedIndex = PlayerPrefs.GetInt("resolution_index", currentIndex);
        savedIndex = Mathf.Clamp(savedIndex, 0, filtered.Count - 1);

        bool savedFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        fullscreenToggle.isOn = savedFullscreen;

        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();

        ApplyResolution(savedIndex, savedFullscreen);
    }

    public void OnResolutionChanged(int index)
    {
        bool fullscreen = fullscreenToggle.isOn;
        ApplyResolution(index, fullscreen);

        PlayerPrefs.SetInt("resolution_index", index);
        PlayerPrefs.Save();
    }

    public void OnFullsreenToggle(bool isFullScreen)
    {
        int index = resolutionDropdown.value;
        ApplyResolution(index, isFullScreen);

        PlayerPrefs.SetInt("fullscreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyResolution(int index, bool fullscreen)
    {
        var r = filtered[index];

        var mode = fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Screen.SetResolution(r.width, r.height, mode);
    }
}
