using UnityEngine.SceneManagement;

public class MainMenuServiceImpl : MainMenuService
{
    public void RedirectToAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void RedirectToGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
