using UnityEngine;
using System.Collections;

public class MainMenuView : MonoBehaviour {
    private IMainMenuService MainMenuService;
	void Start () {
        MainMenuService = new MainMenuService();
	}

    public void PlayClicked()
    {
        print("FUCK");
        GameUIService.Reset();
        MainMenuService.OnPlayClick();
    }

    public void AboutClicked()
    {
        MainMenuService.OnAboutClick();
    }
}
