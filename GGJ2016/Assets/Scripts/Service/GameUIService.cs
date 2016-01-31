﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class GameUIService : IGameUIService
{
    private static TimeModel TimeModel;
    private static PlayerStatsModel PlayerStatsModel;
    private const float TIMER_SPEED = .75f;
    private float timer = 0;
    private bool clockTick = false;
    private bool updateTick = false;

    public bool ClockTick
    {
        get
        {
            return clockTick;
        }
    }

    public GameUIService()
    {
        if (TimeModel == null)
            TimeModel = new TimeModel();
        else
        {
            TimeModel.Hour = 5;
            TimeModel.Minute = 30;
        }
        if(PlayerStatsModel == null)
            PlayerStatsModel = new PlayerStatsModel();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("About");
    }

    public void GoToGameOver()
    {
        GoToMainMenu();
    }

    public void UpdateTimer(float deltaTime)
    {
        timer += deltaTime * TIMER_SPEED;
        clockTick = timer % .25 <= .1f;
        updateTick = false;
        if (timer > 1)
        {
            timer = 0;
            TimeModel.Minute++;
            if (TimeModel.Minute > 59)
            {
                TimeModel.Minute = 0;
                TimeModel.Hour++;
            }
            if (TimeModel.Hour > 23)
            {
                TimeModel.Hour = 0;
                TimeModel.Day++;
            }
            updateTick = true;
        }
    }

    public string GetHour()
    {
        string result = TimeModel.Hour.ToString();
        return result.Length == 1 ? "0" + result : result;
    }

    public string GetMinute()
    {
        string result = TimeModel.Minute.ToString();
        return result.Length == 1 ? "0" + result : result;
    }

    public int GetDay()
    {
        return TimeModel.Day;
    }

    public string GetRandomMonth()
    {
        return TimeModel.Months[Random.Range(0, 11)];
    }

    public float GetHappiness()
    {
        return PlayerStatsModel.Hapiness;
    }

    public void UpdateGame()
    {
        if (updateTick)
        {
            GameOverCheck();
            UpdateHapiness();
        }
    }

    void UpdateHapiness()
    {
        if (TimeModel.TotalMinutes == 7 * 60 + 30)
            PlayerStatsModel.Hapiness -= .1f;
    }

    void GameOverCheck()
    {
        if (PlayerStatsModel.Hapiness == 0)
            GoToGameOver();
        else if (TimeModel.TotalMinutes == 9 * 60)
        {
            PlayerStatsModel.Hapiness -= .1f;
            TimeModel.Day++;
            RestartGame();
        }
    }

    public static void Reset()
    {
        TimeModel = null;
        PlayerStatsModel = null;
    }
}