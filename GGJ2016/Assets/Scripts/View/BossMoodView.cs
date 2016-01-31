using UnityEngine;
using System.Collections;

public class BossMoodView : MonoBehaviour {


    public Texture2D[] moods;
    public Renderer moodSprite;
    private GameUIService UIService;

    void Start() {
        UIService = GameUIService.UIService;
        if (UIService == null)
            UIService = new GameUIService(null);
    }

    public void ShowFeedback() {
        moodSprite.gameObject.SetActive(true);

        int min = UIService.GetTotalMinutes();
        int frame = 0;
        
        if (min > 7 * 60 + 30 && min < 9 * 60)
            frame = 1;
        else if (min > 9 * 60)
            frame = 2;

        moodSprite.material.SetTexture("_MainTex", moods[frame]);

        string[] anims = new string[]{
            "Happy",
            "Normal",
            "Normal"
        };
        GetComponent<Animation>().Play(anims[frame]);
    }
}
