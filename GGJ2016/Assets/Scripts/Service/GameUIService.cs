using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIService : IGameUIService
{
    private static TimeModel TimeModel;
    private static PlayerStatsModel PlayerStatsModel;

    private const float TIMER_SPEED = .75f;

    private float timer = 0;
    private bool clockTick = false;
    private bool updateTick = false;
    private float timerDelay;

    private Image uberOuter;
    private UberModel uberModel;

    private SpawnerView spawnerView;

    #region Getters & Setters
    public SpawnerView SpawnerView
    {
        set
        {
            spawnerView = value;
        }
    }


    public bool ClockTick
    {
        get
        {
            return clockTick;
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

    public string GetMonth()
    {
        return TimeModel.Month;
    }

    public float GetHappiness()
    {
        return PlayerStatsModel.Hapiness;
    }
    #endregion

    #region Static
    public static TimeModel GetTimeModel()
    {
        return TimeModel;
    }
    public static PlayerStatsModel GetPlayerStatsModel()
    {
        return PlayerStatsModel;
    }
    public static void Reset()
    {
        TimeModel = null;
        PlayerStatsModel = null;
    }
    #endregion

    public GameUIService(Image uberOuter)
    {
        if (TimeModel == null)
        {
            TimeModel = new TimeModel();
            TimeModel.Month = TimeModel.Months[Random.Range(0, 11)];
        }
        else
            TimeModel.Day++;

        //Set the wake up hour
        TimeModel.Hour = 6;
        TimeModel.Minute = 0;

        if (PlayerStatsModel == null)
            PlayerStatsModel = new PlayerStatsModel();

        uberModel = new UberModel(false);
        this.uberOuter = uberOuter;

        InputManager.instance.onCameraClickPressedListener += ShowUberUI;
        InputManager.instance.onCameraClickUpListener += HideUberUI;

        if(uberOuter != null)
            uberOuter.gameObject.SetActive(false);
    }

    #region Scene Related
    public void ReloadScene()
    {
        GameManager.GetInstance().ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        GameManager.GetInstance().ChangeScene(SceneType.MainMenu.ToString());
    }

    public void GoToCredits()
    {
        GameManager.GetInstance().ChangeScene(SceneType.About.ToString());
    }

    public void GoToGameOver()
    {
        GameManager.GetInstance().ChangeScene("Cena_Fired");
    }
    #endregion

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

    public void ShowUberUI(Vector3 cameraPosition)
    {
        if (uberModel != null)
        {
            uberModel.SetRunAnimation(true);
            if (DelayToAnim())
            {
                uberOuter.gameObject.SetActive(true);
                Vector3 pos;
                if (Application.platform == RuntimePlatform.Android)
                    pos = Input.GetTouch(0).position;
                else
                    pos = Input.mousePosition;
                uberOuter.transform.parent.position = pos;
            }
        }
    }

    public void HideUberUI(Vector3 cameraPosition)
    {
        if (uberModel != null)
        {
            uberModel.SetRunAnimation(false);
            uberOuter.gameObject.SetActive(false);
            ClearDelay();
        }
    }

    public void SetupUberFillAmount(Image image)
    {
        float speed = Time.deltaTime * 0.5f;

        image.fillAmount = (uberModel.isRunning() && DelayToAnim()) ? image.fillAmount + speed : 0;

        CallUber(image.fillAmount);
    }

    public void CallUber(float fillAmount)
    {
        if (fillAmount >= 1)
        {
            if (spawnerView != null)
            {
                Transform dir = spawnerView.SpawnPositionRight;
                GameObject o = spawnerView.PoolInstantiate(SpawnerView.EntityType.Uber, dir.position, dir.rotation);
                if (o != null)
                {
                    TransportEntityView v = o.GetComponent<TransportEntityView>();
                    v.Speed = 3 + Random.Range(0f, 1f);
                }
            }
        }
    }

    public void UpdateGame()
    {
        if (updateTick)
        {
            GameOverCheck();
            EndOfTheDayCheck();
        }
    }

    void GameOverCheck()
    {
        if (PlayerStatsModel.Hapiness == 0)
            GoToGameOver();
    }

    void EndOfTheDayCheck()
    {
        if (BossMoodView.GetMood(GetTotalMinutes()) == 2)
        {
            PlayerStatsModel.Hapiness -= .2f;
            ReloadScene();
        }
    }

    bool DelayToAnim()
    {
        if (timerDelay < 2)
            timerDelay += Time.deltaTime * 1f;

        return timerDelay >= 2;

    }

    private void ClearDelay()
    {
        timerDelay = 0;
    }
}