using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningTime : MonoBehaviour
{
    public Text Timer;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;

    void Update()
    {
        UpdateTimer();
    }

    public void UpdateTimer()
    {
        secondsCount += Time.deltaTime;

        int seconds = (int)secondsCount % 60;
        int minutes = (int)secondsCount / 60 % 60;
        int hours = (int)secondsCount / 3600;

        string formattedHour = hours.ToString("D2");
        string formattedMinutes = minutes.ToString("D2");
        string formattedSeconds = seconds.ToString("D2");

        Timer.text = "Running " + formattedHour + ":" + formattedMinutes + ":" + formattedSeconds;
    }
}
