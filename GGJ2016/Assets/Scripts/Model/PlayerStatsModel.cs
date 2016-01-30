using UnityEngine;
using System.Collections;

public class PlayerStatsModel {
    private float hapiness = 1f;
    public float Hapiness
    {
        get
        {
            return hapiness;
        }
        set
        {
            if (value > 1)
                value = 1;
            else if (value < 0)
                value = 0;
            hapiness = value;
        }
    }
}
