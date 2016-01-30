using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIView : MonoBehaviour {

    public Text Day;
    public Text Month;
    public Text Clock;
    public Image BossIndicator;
    public PauseMenuView PauseMenuView;
    private IGameUIService Service;

	void Start () {
        Service = new GameUIService();
        UpdateMonthView();
	}

    void Update()
    {
        Service.UpdateTimer(Time.deltaTime);
        UpdateClockView();
        UpdateDayView();
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

    public IGameUIService GetService()
    {
        return Service;
    }
}
