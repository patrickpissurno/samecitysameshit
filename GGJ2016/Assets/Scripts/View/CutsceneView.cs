using UnityEngine;
using System.Collections;

public class CutsceneView : MonoBehaviour
{
    private TimeModel timeModel;

    [Header("Time Fastforward (minutes)")]
    [Tooltip("The amount of time to add (in minutes)")]
    [Range(0, 120)]
    [SerializeField]
    private int timeToAdd = 0;

	private void Start () {
        timeModel = GameUIService.GetTimeModel();
        if (timeModel != null)
            TimeFastforward();
	}
	
	private void TimeFastforward()
    {
        timeModel.Minute += timeToAdd;
    }
}
