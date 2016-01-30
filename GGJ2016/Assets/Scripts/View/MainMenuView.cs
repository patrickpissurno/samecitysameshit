using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuView : MonoBehaviour {
    private IMainMenuService MainMenuService;
    public Text text;
	void Start () {
        MainMenuService = new MainMenuService();
	}

    void PlayClicked()
    {
        MainMenuService.OnPlayClick();
    }

    void AboutClicked()
    {
        MainMenuService.OnAboutClick();
    }
}
