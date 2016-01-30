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
        
        Vector4 bounds = CameraHelper.GetBounds(GameManager.getBean<Camera>());
        Transform spawn = GameManager.getBean<PrefabManager>().LoadPrefab(component.element.ToString());

        float z = (component.side == StreetSide.LeftSide) ? -6.56f : -5.6f;
        float x = (component.side == StreetSide.LeftSide) ? bounds.x : bounds.z;

        float bound = (component.side == StreetSide.LeftSide) ? (bounds.z + 5)  : (bounds.x - 5);
        float speed = (component.side == StreetSide.LeftSide) ? -component.speed : component.speed;

        Transform t = Instantiate(spawn, new Vector3(x, spawn.position.y, z), spawn.rotation) as Transform;
        
        Vector3 angle = t.eulerAngles;
        angle.y = (component.side == StreetSide.LeftSide) ? 180 : 0;
        t.eulerAngles = angle;
        t.name = component.element.ToString() + " - " + Random.Range(0, 10000);
        t.gameObject.AddComponent<MovementBehaviorView>().Init(bound, component.speed);
    }
}
