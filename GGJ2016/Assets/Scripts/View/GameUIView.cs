using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour {

    private IGameUIService UIService;

    public Text day;
    public Text month;
    public Text clock;
    public Image bossIndicator;
    public Image uberOuter;
    public Image uberInner;

    public LightView lightView;
    public SpawnerView spawnerView;
    public PauseMenuView pauseMenuView;
    public GameView gameView;

    private static Sprite[] bossSprites;
    private int bossSpriteIndex = 0;

	void Start () {
        UIService = new GameUIService(uberOuter);

        //Init Light View
        if(lightView != null)
            lightView.Service = UIService;

        //Init Spawner View
        if (spawnerView != null)
        {
            spawnerView.Service = UIService;
            UIService.SpawnerView = spawnerView;
            spawnerView.DelayedStart();
        }

        //Init Game View
        if(gameView != null)
        {
            gameView.SetupUIService(UIService);
        }

        LoadBossSprites();
        UpdateMonthView();
	}

    void Update()
    {
        //Service Update
        UIService.UpdateTimer(Time.deltaTime);
        UIService.UpdateGame();

        //User Interface Update
        UpdateClockView();
        UpdateDayView();
        SetupRebuFillAmount();
        UpdateBossSprite();
    }

    public void PauseButtonClick()
    {
        pauseMenuView.Show();
    }

    void UpdateClockView()
    {
        clock.text = UIService.GetHour() + (UIService.ClockTick ? ":" : " ") + UIService.GetMinute();
    }

    void UpdateDayView()
    {
        day.text = UIService.GetDay().ToString();
    }

    void UpdateMonthView()
    {
        month.text = UIService.GetMonth();
    }

    void UpdateBossSprite()
    {
        int spriteIndex = Mathf.FloorToInt(8 - 8 * UIService.GetHappiness());
        if (spriteIndex > 7)
            spriteIndex = 7;
        if (bossSpriteIndex != spriteIndex)
        {
            bossSpriteIndex = spriteIndex;
            bossIndicator.sprite = bossSprites[bossSpriteIndex];
        }
    }
    
    public void SetupRebuFillAmount()
    {
        UIService.SetupUberFillAmount(uberInner);
    }

    void LoadBossSprites()
    {
        if(bossSprites == null)
            bossSprites = Resources.LoadAll<Sprite>("Sprites/boss");
    }
}
