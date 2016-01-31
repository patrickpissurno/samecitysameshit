using UnityEngine.SceneManagement;

public class MainMenuService : IMainMenuService
{
    public void OnPlayClick()
    {
        GameManager.getInstance().ChangeScene("CutsceneInicial");
    }

    public void OnAboutClick()
    {
        GameManager.getInstance().ChangeScene("About");
    }
}