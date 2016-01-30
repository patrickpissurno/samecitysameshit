using UnityEngine.SceneManagement;

public class AboutMenuController : IAboutMenuController
{
    public void AnimationEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}