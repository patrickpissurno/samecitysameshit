using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour {

    public Text Day;
    public Text Month;
    public Text Clock;
    public Image BossIndicator;
    public PauseMenuView PauseMenuView;
    private IGameUIService Service;
    private static Sprite[] BossSprites;
    private int BossSpriteIndex = 0;
    public LightView LightView;
    public SpawnerView SpawnerView;

	void Start () {
        Service = new GameUIService();
        if(LightView != null)
            LightView.Service = Service;
        if (SpawnerView != null)
        {
            SpawnerView.Service = Service;
            SpawnerView.DelayedStart();
        }
        LoadBossSprites();
        UpdateMonthView();
	}

    void Update()
    {
        Service.UpdateTimer(Time.deltaTime);
        Service.UpdateGame();
        UpdateClockView();
        UpdateDayView();
        SetupRebuFillAmount();
        UpdateBossSprite();
    }

    public void PauseButtonClick()
    {
        PauseMenuView.Show();
    }

    void UpdateClockView()
    {
        Clock.text = Service.GetHour() + (Service.ClockTick ? ":" : " ") + Service.GetMinute();
    }

    void UpdateDayView()
    {
        Day.text = Service.GetDay().ToString();
    }

    void UpdateMonthView()
    {
        Month.text = Service.GetRandomMonth();
    }

    void UpdateBossSprite()
    {
        int spriteIndex = Mathf.FloorToInt(8 - 8 * Service.GetHappiness());
        if (spriteIndex > 7)
            spriteIndex = 7;
        if (BossSpriteIndex != spriteIndex)
        {
            BossSpriteIndex = spriteIndex;
            BossIndicator.sprite = BossSprites[BossSpriteIndex];
        }
    }

    public IGameUIService GetService()
    {
        return Service;
    }
    
    public void SetupRebuFillAmount()
    {
        Service.SetupRebuFillAmount(GameObject.Find(ElementType.Rebu.ToString()));
    }

    void LoadBossSprites()
    {
        if(BossSprites == null)
            BossSprites = Resources.LoadAll<Sprite>("Sprites/boss");
    }
}
