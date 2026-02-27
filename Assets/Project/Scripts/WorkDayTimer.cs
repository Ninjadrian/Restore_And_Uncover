using UnityEngine;
using TMPro;

public class WorkDayTimer : MonoBehaviour
{
    public float realSecondsPerGameMinute = 1f;
    private int workDayHours = 8;
    private int startHour = 0;

    public int autosaveEveryMinutes = 5;

    public TMP_Text clockText;
    public TMP_Text dayText;

    private float timer;
    private int currentMinute = 0;
    private int currentHour = 0;
    private int currentDay = 1;

    private int maxMinutes;

    private void Start()
    {
        LoadFromProfile();
        maxMinutes = workDayHours * 60;
        UpdateUI();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= realSecondsPerGameMinute)
        {
            timer = 0f;
            AddMinute();
        }
    }

    private void UpdateUI()
    {
        clockText.text = $"{currentHour:00}:{currentMinute:00}/08:00";
        dayText.text = "Día " + currentDay;
    }

    private void AddMinute()
    {
        currentMinute++;
        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        int totalMinutes = currentHour * 60 + currentMinute;

        if (totalMinutes >= maxMinutes)
        {
            EndDay();
        }

        int minutes = GetMinutesOfDay();
        PlayerProfiler.Instance.data.time = minutes;
        PlayerProfiler.Instance.data.day = currentDay;

        if (minutes % autosaveEveryMinutes == 0)
            PlayerProfiler.Instance.SaveProfile();

        UpdateUI();
    }

    private void EndDay()
    {
        currentDay++;
        currentHour = 0;
        currentMinute = 0;

        PlayerProfiler.Instance.data.day = currentDay;
        PlayerProfiler.Instance.data.time = 0;

        PlayerProfiler.Instance.SaveProfile();
    }

    private void LoadFromProfile()
    {
        var prof = PlayerProfiler.Instance;
        if (prof == null) return;

        //Si no existe
        if (prof.data.day <= 0) prof.data.day = 1;

        if (prof.data.time < 0) prof.data.time = 0;

        ApplyTime(prof.data.time);
    }

    private void SaveToProfile()
    {
        var prof = PlayerProfiler.Instance;
        if (prof == null) return;

        prof.data.day = currentDay;
        prof.data.time = GetMinutesOfDay();

        prof.SaveProfile();
    }

    private void ApplyTime(int minutesOfDay)
    {
        minutesOfDay = Mathf.Clamp(minutesOfDay, 0, workDayHours * 60);

        currentHour = startHour + (minutesOfDay / 60);
        currentMinute = minutesOfDay % 60;
        currentDay = Mathf.Max(1, PlayerProfiler.Instance.data.day);
    }

    int GetMinutesOfDay()
    {
        return (currentHour - startHour) * 60 + currentMinute;
    }
}
