using UnityEngine.SceneManagement;

public class MainMenuService : IMainMenuService
{
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnAboutClick()
    {
        SceneManager.LoadScene("About");
    }
}