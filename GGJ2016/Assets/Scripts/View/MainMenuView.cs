using UnityEngine;

public class MainMenuView : MonoBehaviour {
    private IMainMenuService MainMenuService;
    public Transform mainAudio;

	void Start ()
    {
        MainMenuService = new MainMenuService();
        InitializeBackgroundMusic();
	}

    public void PlayClicked()
    {
        GameUIService.Reset();
        MainMenuService.OnPlayClick();
    }

    public void AboutClicked()
    {
        MainMenuService.OnAboutClick();
    }

    private void InitializeBackgroundMusic()
    {
        if (!GameObject.Find("Audio_Prefab_Component"))
        {
            Transform t = Instantiate(mainAudio, mainAudio.position, mainAudio.rotation) as Transform;
            t.name = "Audio_Prefab_Component";
            DontDestroyOnLoad(t.gameObject);
        }
    }
}
