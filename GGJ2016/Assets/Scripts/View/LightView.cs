using UnityEngine;

public class LightView : MonoBehaviour {
    public IGameUIService Service;
    private const float DAY_TOTAL_MINUTES = 24 * 60;

	void Update () {
        if (Service != null)
        {
            Quaternion targetRot = Quaternion.Euler((Service.GetTotalMinutes() / DAY_TOTAL_MINUTES * 360f) - 90, 170, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime);
        }
	}
}
