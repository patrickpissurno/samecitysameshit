using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIService : IGameUIService
{
    private static TimeModel TimeModel;
    private static PlayerStatsModel PlayerStatsModel;
    private const float TIMER_SPEED = 8f;//.75f;

    private float timer = 0;
    private bool clockTick = false;
    private bool updateTick = false;

    private GameObject RebuBG;
    private RebuModel RebuObject;

    private float timerDelay;


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

        if (PlayerStatsModel == null)
            PlayerStatsModel = new PlayerStatsModel();

        RebuObject = new RebuModel(false);

        InputManager.onCameraClickPressedListener += ShowUberUI;
        InputManager.onCameraClickUpListener += HideUberUI;

        RebuBG = GameObject.Find(ElementType.RebuBG.ToString());
        RebuBG.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneType.Game.ToString());
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneType.MainMenu.ToString());
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(SceneType.About.ToString());
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

    public int GetTotalMinutes()
    {
        return TimeModel.TotalMinutes;
    }

    public int GetDay()
    {
        return TimeModel.Day;
    }

    public string GetRandomMonth()
    {
        return TimeModel.Months[Random.Range(0, 11)]; ;
    }

    public void ShowUberUI(Vector3 cameraPosition)
    {
        if (RebuObject != null)
        {
            RebuObject.setRunAnimation(true);

            if (delayToAnim())
                RebuBG.SetActive(true);
        }
    }

    public void HideUberUI(Vector3 cameraPosition)
    {
        if (RebuObject != null)
        {
            RebuObject.setRunAnimation(false);
            RebuBG.SetActive(false);
            clearDelay();
        }
    }

    public void SetupRebuFillAmount(GameObject gameObject)
    {
        float speed = Time.deltaTime * 0.5f;

        gameObject.GetComponent<Image>().fillAmount = (RebuObject.isRunning() && delayToAnim()) ?
            gameObject.GetComponent<Image>().fillAmount + speed
            : 0;
    }

    public void CallToRebu(float fillAmount)
    {
        if (fillAmount >= 1)
        {
            //call to rebu
        }
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

    bool delayToAnim()
    {
        if (timerDelay < 2)
            timerDelay += Time.deltaTime * 1f;

        return timerDelay >= 2;

    }

    private void clearDelay()
    {
        timerDelay = 0;
    }

    public static void Reset()
    {
        TimeModel = null;
        PlayerStatsModel = null;
    }
}