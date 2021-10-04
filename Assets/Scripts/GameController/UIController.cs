using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public TMP_Text progressDisplay;

    public TMP_Text timerDisplay;

    public TMP_Text splitTimerDisplay;

    public TMP_Text scoreDisplay;


    public TMP_Text finalScoreDisplay;

    public TMP_Text finalTimeDisplay;

    public TMP_Text splitTimeDisplay;

    public GameObject inGameUI;

    public GameObject statUI;

    

    private void Start()
    {
        GameController.Instance.OnGameReset += OnGameReset;
        GameController.Instance.OnGameWon += OnGameWon;
    }



    private void Update()
    {
        DisplayProgress();
        DisplayTime();
        DisplayScore();
    }

    private void DisplayScore()
    {
        scoreDisplay.text = $"Score: {string.Format("{0:0000}",GameController.Instance.Score)}";
    }

    private void DisplayProgress()
    {
        progressDisplay.text = $"{(GameController.Instance.LevelProgress * 100f).ToString("F2")}m";
    }

    private void DisplayTime()
    {
        timerDisplay.text = FormatedTimer(GameController.Instance.Timer);
        splitTimerDisplay.text = FormatedTimer(GameController.Instance.SplitTimer);
    }

    private string FormatedTimer(float time)
    {  
        int minutes = (int)time / 60;
        int seconds = (int)time - (minutes * 60); //121 => 121 - (2 * 60) = 121 - 120 = 1
        int milliseconds = (int)((time - (minutes * 60) - seconds) * 1000f);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void OnGameWon()
    {
        inGameUI.SetActive(false);
        statUI.SetActive(true);

        finalScoreDisplay.text = string.Format("Score score {0}", GameController.Instance.Score);
        splitTimeDisplay.text = string.Format("Split time {0}", FormatedTimer(GameController.Instance.SplitTimer));
        finalTimeDisplay.text = string.Format("Game time {0}", FormatedTimer(GameController.Instance.Timer));
    }

    private void OnGameReset()
    {
        inGameUI.SetActive(true);
        statUI.SetActive(false);
    }

    public void OpenDiscordBrowser()
    {
        Application.OpenURL("https://discord.gg/ktfaY6Vpqf");
    }
}
