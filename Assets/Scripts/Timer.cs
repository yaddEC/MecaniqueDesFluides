using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60f; // Temps limite en secondes
    private float timeRemaining;
    private bool isTimerRunning = true;
    public bool isTimerRunning2;
    private Text timerText;

    private void Start()
    {
        timerText = GetComponent<Text>();
        ResetTimer();
    }

    private void Update()
    {
        if (isTimerRunning && isTimerRunning2)
        {
            timeRemaining += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StartTimer()
    {
        isTimerRunning = !isTimerRunning;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        isTimerRunning2 = false;
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
