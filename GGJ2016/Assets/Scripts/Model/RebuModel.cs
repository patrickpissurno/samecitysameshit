using UnityEngine;
using System.Collections;

public class RebuModel {

    private bool canRunAnimation;

    public RebuModel(bool canRunAnimation)
    {
        this.canRunAnimation = canRunAnimation;
    }

    public void SetRunAnimation(bool canRunAnimation)
    {
        this.canRunAnimation = canRunAnimation;
    }

    public bool isRunning() { return canRunAnimation; }
}
