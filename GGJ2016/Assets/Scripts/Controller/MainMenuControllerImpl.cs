public class MainMenuControllerImpl : MainMenuController {
    private MainMenuService MainMenuService;

    public MainMenuControllerImpl()
    {
        MainMenuService = new MainMenuServiceImpl();
    }
    public void RedirectToAbout()
    {
        MainMenuService.RedirectToAbout();
    }

    public void RedirectToGame()
    {
        MainMenuService.RedirectToGame();
    }
}
