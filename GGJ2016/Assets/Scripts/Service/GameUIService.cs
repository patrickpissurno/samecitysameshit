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

    private Image RebuBG;
    private RebuModel RebuObject;

    private SpawnerView spawnerView;

    #region Get & Set
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

    public GameUIService(Image RebuBG)
    {
        if (TimeModel == null)
        {
            TimeModel = new TimeModel();
            TimeModel.Month = TimeModel.Months[Random.Range(0, 11)];
        }
        else
        {
            TimeModel.Hour = 6;
            TimeModel.Minute = 20;
            TimeModel.Day++;
        }

        if (PlayerStatsModel == null)
            PlayerStatsModel = new PlayerStatsModel();

        RebuObject = new RebuModel(false);
        this.RebuBG = RebuBG;

        InputManager.instance.onCameraClickPressedListener += ShowUberUI;
        InputManager.instance.onCameraClickUpListener += HideUberUI;

        if(RebuBG != null)
        RebuBG.gameObject.SetActive(false);
    }

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

    public string GetMonth()
    {
        return TimeModel.Month;
    }

    public void ShowUberUI(Vector3 cameraPosition)
    {
        if (RebuObject != null)
        {
            RebuObject.SetRunAnimation(true);
            if (DelayToAnim())
            {
                RebuBG.gameObject.SetActive(true);
                Vector3 pos;
                if (Application.platform == RuntimePlatform.Android)
                    pos = Input.GetTouch(0).position;
                else
                    pos = Input.mousePosition;
                RebuBG.transform.parent.position = pos;
            }
        }
    }

    public void HideUberUI(Vector3 cameraPosition)
    {
        if (RebuObject != null)
        {
            RebuObject.SetRunAnimation(false);
            RebuBG.gameObject.SetActive(false);
            ClearDelay();
        }
    }

    public void SetupUberFillAmount(Image image)
    {
        float speed = Time.deltaTime * 0.5f;

        image.fillAmount = (RebuObject.isRunning() && DelayToAnim()) ? image.fillAmount + speed : 0;

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