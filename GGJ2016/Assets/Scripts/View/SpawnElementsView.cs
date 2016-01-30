using UnityEngine;
using System.Collections;

public class SpawnElementsView : MonoBehaviour {

    [Range(0,1)]
    public float CarPercentage;

    [Range(0, 1)]
    public float UberPercentage;

    [Range(0, 1)]
    public float TrainPercentage;

    [Range(0, 1)]
    public float BusPercentage;

    [Range(0, 1)]
    public float TaxiPercentage;

    public SpawnElementsModel currentElement;

    void Start() {
        Init();
    }

    public void Init() {
        SpawnElementsService.Instance.OnElementGenerated = OnElementGenerated;
        SpawnElementsService.Instance.Initialize(this);
    }

    public void OnElementGenerated(SpawnElementsModel component) {
        currentElement = component;
    }
}
