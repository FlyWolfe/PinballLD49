using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public TMP_Text progressDisplay;

    public TMP_Text timerDisplay;

    public TMP_Text scoreDisplay;

    public TMP_Text finalTimeDisplay;
    
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
        progressDisplay.text = $"{(GameController.Instance.LevelProgress * 100f).ToString("F2")}%";
    }

    private void DisplayTime()
    {
        timerDisplay.text = FormatedTimer();
    }

    private string FormatedTimer()
    {
        float gameTime = GameController.Instance.Timer;
        int minutes = (int)gameTime / 60;
        int seconds = (int)gameTime - (minutes * 60); //121 => 121 - (2 * 60) = 121 - 120 = 1
        int milliseconds = (int)((gameTime - (minutes * 60) - seconds) * 1000f);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void OnGameWon()
    {
        inGameUI.SetActive(false);
        statUI.SetActive(true);

        finalTimeDisplay.text = string.Format("You've finished the game in {0}", FormatedTimer());
    }

    private void OnGameReset()
    {
        inGameUI.SetActive(true);
        statUI.SetActive(false);
    }
}
