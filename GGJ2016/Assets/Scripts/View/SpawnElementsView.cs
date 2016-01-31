using UnityEngine;
using System.Collections.Generic;

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
    public Dictionary<SpawnType, List<ObjectPoolService>> allObjects = new Dictionary<SpawnType, List<ObjectPoolService>>();

    private const int MIN_OBJECTS = 15;

    void Start() {
        BeginObjectPool();
        Init();
    }

    public void Init() {
        SpawnElementsService.Instance.OnElementGenerated = OnElementGenerated;
        SpawnElementsService.Instance.Initialize(this);
    }

    public void BeginObjectPool() {

        CreateInitialObjects(SpawnType.Bus);
        CreateInitialObjects(SpawnType.Car0);
        CreateInitialObjects(SpawnType.Car1);
        CreateInitialObjects(SpawnType.Car2);
        CreateInitialObjects(SpawnType.Taxi);
        CreateInitialObjects(SpawnType.Uber);
    }

    public void CreateInitialObjects(SpawnType type) {
        GameObject g = new GameObject(type.ToString() + "Container");
        for (int i = 0; i < MIN_OBJECTS; i++) {

            Transform spawn = GameManager.getBean<PrefabManager>().LoadPrefab(type.ToString());
            Vector4 bounds = CameraHelper.GetBounds(GameManager.getBean<Camera>());

            Transform t = Instantiate(spawn, new Vector3(bounds.z, spawn.position.y, 17), spawn.rotation) as Transform;
            t.parent = g.transform;

            ObjectPoolService pool = new ObjectPoolService() {
                component = t
            };

            pool.SetActive(false);

            if (allObjects.ContainsKey(type)) {
                if(allObjects[type] == null) {
                    allObjects[type] = new List<ObjectPoolService>();
                }

                allObjects[type].Add(pool);

            } else {
                allObjects.Add(type, new List<ObjectPoolService>());
                allObjects[type].Add(pool);
            }
        }
    }

    public void OnElementGenerated(SpawnElementsModel component) {
        currentElement = component;
        
        Vector4 bounds = CameraHelper.GetBounds(GameManager.getBean<Camera>());
        Transform spawn = GameManager.getBean<PrefabManager>().LoadPrefab(component.element.ToString());

        float z = (component.side == StreetSide.LeftSide) ? 20 : 17;
        float x = (component.side == StreetSide.LeftSide) ? bounds.x : bounds.z;

        float bound = (component.side == StreetSide.LeftSide) ? (bounds.z + 5)  : (bounds.x - 5);
        float speed = (component.side == StreetSide.LeftSide) ? -component.speed : component.speed;

        ObjectPoolService objectPool = GetElementFromType(component.element);
        Transform t = objectPool.component;

        string objectName = component.element.ToString() + " - " + Random.Range(0, 10000);
        objectPool.Reset(new Vector3(x, spawn.position.y, z), (component.side == StreetSide.LeftSide) ? 180 : 0, objectName);

        if (t.GetComponent<MovementBehaviorView>()) {
            t.GetComponent<MovementBehaviorView>().Init(bound, component.speed);
        } else {
            t.gameObject.AddComponent<MovementBehaviorView>().Init(bound, component.speed);
            t.GetComponent<MovementBehaviorView>().poolComponent = objectPool;
        }
    }

    public ObjectPoolService GetElementFromType(SpawnType type) {

        if (!allObjects.ContainsKey(type)) {
            throw new System.ArgumentNullException("The requested Key from Dictionary is empty. Check if assignable pool object has been created in Start");
        }

        foreach (ObjectPoolService item in allObjects[type]) {
            if (!item.InUse) {
                return item;
            }
        }

        throw new System.IndexOutOfRangeException("Isn't possible to reset object from list. All objects still in use, you need create more objects.");
    }

}
