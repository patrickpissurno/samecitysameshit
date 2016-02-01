using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour {

    private IGameUIService UIService;

    //[SerializeField] makes a variable appear in the Unity Inspector
    //without the need to make it public. It's kinda the oposite of
    //[HideInInspector]

    [SerializeField]
    private Text day;
    [SerializeField]
    private Text month;
    [SerializeField]
    private Text clock;
    [SerializeField]
    private Image bossIndicator;
    [SerializeField]
    private Image uberOuter;
    [SerializeField]
    private Image uberInner;

    [SerializeField]
    private LightView lightView;
    [SerializeField]
    private SpawnerView spawnerView;
    [SerializeField]
    private PauseMenuView pauseMenuView;
    [SerializeField]
    private GameView gameView;

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
