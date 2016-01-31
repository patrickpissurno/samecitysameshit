using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUIService : IGameUIService
{
    private TimeModel Model;
    private RebuModel RebuObject;
    private const float TIMER_SPEED = .25f;
    private float timer = 0;
    private bool clockTick = false;

    private GameObject RebuBG;

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
        RebuObject = new RebuModel(false);

        InputManager.onCameraClickPressedListener += ShowUberUI;
        InputManager.onCameraClickUpListener += HideUberUI;

        RebuBG = GameObject.Find(ElementType.RebuBG.ToString());
        RebuBG.SetActive(false);
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
        return Model.Months[UnityEngine.Random.Range(0, 11)];
    }

    public void ShowUberUI(Vector3 cameraPosition)
    {
        if (RebuObject != null)
        {
            RebuObject.setRunAnimation(true);
            RebuBG.SetActive(true);
        }
    }

    public void HideUberUI(Vector3 cameraPosition)
    {
        if (RebuObject != null)
        {
            RebuObject.setRunAnimation(false);
            RebuBG.SetActive(false);
        }
    }

    public void SetupRebuFillAmount(GameObject gameObject)
    {
        float speed = Time.deltaTime * 0.5f;

        gameObject.GetComponent<Image>().fillAmount = (RebuObject.isRunning()) ?
            gameObject.GetComponent<Image>().fillAmount + speed
            : 0;

        Debug.Log(gameObject.GetComponent<Image>().fillAmount);
    }

    public void callToRebu(float fillAmount)
    {
        if (fillAmount >= 1)
        {
            //call to rebu
        }
    }
}