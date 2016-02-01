using UnityEngine.SceneManagement;

public class MainMenuService : IMainMenuService
{
    public void OnPlayClick()
    {
        GameManager.GetInstance().ChangeScene("CutsceneInicial");
    }

    public void OnAboutClick()
    {
        GameManager.GetInstance().ChangeScene("About");
    }
}