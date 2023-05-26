using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estimated : MonoBehaviour
{
    private bool isTimerRunning = false;
    private bool RNG;
    private float timeRemaining;
    private float estimaedTime;
    private float bigSurgface;
    private float smallSurgface;
    private Text timerText;
    public WaterTower waterTower;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
        ResetTimer();
    }
    private void Update()
    {
        if (isTimerRunning)
        {
            if(RNG)
            {
                bigSurgface = (Mathf.PI * (waterTower.width * waterTower.width)) / 4;
                smallSurgface = (Mathf.PI * (waterTower.holeSize * waterTower.holeSize)) / 4;
                estimaedTime = ((2 * bigSurgface) / smallSurgface) * Mathf.Sqrt((waterTower.height) / (2 * 9.81f));
                RNG = false;
            }

            timeRemaining = estimaedTime;
            UpdateTimerDisplay();
        }
        
    }

    public void StartEstimated()
    {
        isTimerRunning = !isTimerRunning;
        RNG = true;
    }

    public void ResetTimer()
    {
        timeRemaining = 0f;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
