using UnityEngine;

public class BossMoodView : MonoBehaviour
{
    public const int JOB_TIME = 7 * 60 + 20;
    public const int END_OF_DAY_TIME = 8 * 60 + 20;

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

            int bossMood = GetMood(timeModel.TotalMinutes);

            moodSprite.material.SetTexture("_MainTex", moods[bossMood]);

            string[] anims = new string[] {
                "Happy",
                "Normal",
                "Normal"
            };

            GetComponent<Animation>().Play(anims[bossMood]);

            UpdateHapiness(bossMood);
        }
    }

    private void UpdateHapiness(int mood)
    {
        switch(mood)
        {
            case 1:
                playerStatsModel.Hapiness -= .1f;
                break;
            case 2:
                playerStatsModel.Hapiness -= .2f;
                break;
        }
        Debug.Log("Time: " + timeModel.Hour + "h " + timeModel.Minute + "min");
    }

    /// <summary>
    /// Returns the mood of the boss based on the Time
    /// </summary>
    /// <param name="totalMinutes">TimeModel instance.TotalMinutes property</param>
    /// <returns>0 = Happy, 1 = Bored, 2 = Angry</returns>
    public static int GetMood(int totalMinutes)
    {
        if (totalMinutes > JOB_TIME && totalMinutes < END_OF_DAY_TIME)
            return 1;
        else if (totalMinutes > END_OF_DAY_TIME)
            return 2;
        else
            return 0;
    }
}
