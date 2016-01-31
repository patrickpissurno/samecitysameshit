using UnityEngine.SceneManagement;

public class AboutMenuService : IAboutMenuService
{
    public void AnimationEnd()
    {
        SceneManager.LoadScene("MainMenu");
    }
}