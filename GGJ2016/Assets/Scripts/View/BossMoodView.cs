using UnityEngine;

public class BossMoodView : MonoBehaviour {


    public Texture2D[] moods;
    public Renderer moodSprite;
    private TimeModel timeModel;
    private PlayerStatsModel playerStatsModel;

    public bool isFiredScene;

    void Start()
    {
        timeModel = GameUIService.GetTimeModel();
        playerStatsModel = GameUIService.GetPlayerStatsModel();
    }

    public void ShowFeedback()
    {
        if (timeModel != null && playerStatsModel != null)
        {
            moodSprite.gameObject.SetActive(true);

            if (isFiredScene)
            {
                moodSprite.material.SetTexture("_MainTex", moods[2]);
                GetComponent<Animation>().Play("Angry");
                return;
            }

            int min = timeModel.TotalMinutes;
            int frame = 0;

            if (min > 7 * 60 + 30 && min < 9 * 60)
                frame = 1;
            else if (min > 9 * 60)
                frame = 2;

            moodSprite.material.SetTexture("_MainTex", moods[frame]);

            string[] anims = new string[] {
                "Happy",
                "Normal",
                "Normal"
            };

            GetComponent<Animation>().Play(anims[frame]);

            UpdateHapiness(frame);
        }
    }

    private void UpdateHapiness(int frame)
    {
        if (frame == 2)
            playerStatsModel.Hapiness -= .1f;
    }
}
