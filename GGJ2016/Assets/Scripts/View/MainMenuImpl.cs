using UnityEngine;
using System;

public class MainMenuImpl : MonoBehaviour, MainMenuView {
    private MainMenuController MainMenuController;

    // Use this for initialization
    void Start()
    {
        MainMenuController = new MainMenuControllerImpl();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AboutButtonClick()
    {
        MainMenuController.RedirectToAbout();
    }

    public void PlayButtonClick()
    {
        MainMenuController.RedirectToGame();
    }

    public void ShowAboutButton()
    {
        throw new NotImplementedException();
    }

    public void ShowBackground()
    {
        throw new NotImplementedException();
    }

    public void ShowPlayButton()
    {
        throw new NotImplementedException();
    }

}
