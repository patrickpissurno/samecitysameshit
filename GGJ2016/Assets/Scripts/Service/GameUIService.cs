using UnityEngine.SceneManagement;
using UnityEngine;

public class GameUIService : IGameUIService
{
    private TimeModel Model;
    private const float TIMER_SPEED = .25f;
    private float timer = 0;
    private bool clockTick = false;

    public bool ClockTick
    {
        get
        {
            return clockTick;
        }
    }

    public GameUIService()
    {
        Model = new TimeModel();
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

    public void UpdateTimer(float deltaTime)
    {
        timer += deltaTime * TIMER_SPEED;
        clockTick = timer % .25 <= .1f;
        if (timer > 1)
        {
            timer = 0;
            Model.Minute++;
            if (Model.Minute > 59)
            {
                Model.Minute = 0;
                Model.Hour++;
            }
            if (Model.Hour > 23)
            {
                Model.Hour = 0;
                Model.Day++;
            }
        }
    }

    public string GetHour()
    {
        string result = Model.Hour.ToString();
        return result.Length == 1 ? "0" + result : result;
    }

    public string GetMinute()
    {
        string result = Model.Minute.ToString();
        return result.Length == 1 ? "0" + result : result;
    }

    public int GetDay()
    {
        return Model.Day;
    }

    public string GetRandomMonth()
    {
        return Model.Months[Random.Range(0, 11)];
    }
}