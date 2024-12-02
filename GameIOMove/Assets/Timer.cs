using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider sliderTimer;
    public Text txTimer;
    public float gameTime = 9999f;
    public bool stopTimer;

    private bool isPaused;
    private float startTime;


    // Start is called before the first frame update

    private void Start()
    {
        stopTimer = false;
        isPaused = false;
        sliderTimer.maxValue = gameTime;
        sliderTimer.value = gameTime;
        startTime = Time.time;
    }

    // Update is called once per frame
    public void TimeCountDown()
    {
        if (isPaused || stopTimer)
        {
            return;
        }
        float elapsedTime = Time.time - startTime;
        float time = gameTime - elapsedTime;
        if (time <= 0)
        {
            stopTimer = true;
            time = 0;
        }

        int second = Mathf.FloorToInt(time);
        string timeString = string.Format("{00}", second);
        txTimer.text = timeString;
        sliderTimer.value = time;
    }
    public void PauseTime()
    {
        isPaused = true;
    }
    public void ResetGameTime()
    {
        gameTime = 9999f;
        sliderTimer.value = gameTime;
        stopTimer = false;
        isPaused = false;
        startTime = Time.time;
    }
}
