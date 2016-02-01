using UnityEngine;
using System.Collections;

public class UberModel {

    private bool canRunAnimation;

    public UberModel(bool canRunAnimation)
    {
        this.canRunAnimation = canRunAnimation;
    }

    public void SetRunAnimation(bool canRunAnimation)
    {
        this.canRunAnimation = canRunAnimation;
    }

    public bool isRunning() { return canRunAnimation; }
}
