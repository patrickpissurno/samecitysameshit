using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerView : MonoBehaviour {
    public IGameUIService Service;

    public Transform SpawnPositionLeft;
    public Transform SpawnPositionRight;

    public Transform SpawnTrainLeft;
    public Transform SpawnTrainRight;

    public GameObject[] Car;
    public GameObject Bus;
    public GameObject Taxi;
    public GameObject Uber;
    public GameObject Train;

    private const int POOL_CAR_AMOUNT = 5;
    private const int POOL_BUS_AMOUNT = 1;
    private const int POOL_TAXI_AMOUNT = 2;
    private const int POOL_UBER_AMOUNT = 1;
    private const int POOL_TRAIN_AMOUNT = 1;

    //The sum of the bellow chances must be equal to 1
    private const float CAR_SPAWN_CHANCE = .7f;
    private const float BUS_SPAWN_CHANCE = .1f;
    private const float TAXI_SPAWN_CHANCE = .2f;
    //This chance is independant
    private const float TRAIN_SPAWN_CHANCE = .4f;

    public List<GameObject> CarPool;
    public List<GameObject> BusPool;
    public List<GameObject> TaxiPool;
    public List<GameObject> UberPool;
    public List<GameObject> TrainPool;

    public enum EntityType
    {
        Car,
        Bus,
        Taxi,
        Uber,
        Train
    }

    void Start()
    {
        CarPool = new List<GameObject>();
        BusPool = new List<GameObject>();
        TaxiPool = new List<GameObject>();
        UberPool = new List<GameObject>();
        TrainPool = new List<GameObject>();
    }

    public void DelayedStart()
    {
        SetupPool(EntityType.Car, CarPool, POOL_CAR_AMOUNT);
        SetupPool(EntityType.Bus, BusPool, POOL_BUS_AMOUNT);
        SetupPool(EntityType.Taxi, TaxiPool, POOL_TAXI_AMOUNT);
        SetupPool(EntityType.Uber, UberPool, POOL_UBER_AMOUNT);
        SetupPool(EntityType.Train, TrainPool, POOL_TRAIN_AMOUNT);
        StartCoroutine(SpawnEntities());
    }

    IEnumerator SpawnEntities()
    {
        int tries = 0;
        bool spawned = false;
        while(tries < 3 && !spawned)
        {
            tries++;
            spawned = SpawnRandomEntity();
        }

        SpawnTrain();
        yield return new WaitForSeconds(2f + Random.Range(-1f, 1f));
        StartCoroutine(SpawnEntities());
    }

    void SpawnTrain()
    {
        float val = Random.Range(0f, 1f);
        if (val < TRAIN_SPAWN_CHANCE)
        {
            Transform Direction = Random.Range(0f, 1f) < .5f ? SpawnTrainLeft : SpawnTrainRight;
            GameObject o = PoolInstantiate(EntityType.Train, Direction.position, Direction.rotation);
            if (o != null)
            {
                TransportEntityView v = o.GetComponent<TransportEntityView>();
                v.Speed = 4 + Random.Range(3f, 5f);
            }
        }
    }

    bool SpawnRandomEntity()
    {
        float val = Random.Range(0f, 1f);
        EntityType Type = EntityType.Car;
        if (val < CAR_SPAWN_CHANCE)
            Type = EntityType.Car;
        else if (val < CAR_SPAWN_CHANCE + BUS_SPAWN_CHANCE)
            Type = EntityType.Bus;
        else if (val < CAR_SPAWN_CHANCE + BUS_SPAWN_CHANCE + TAXI_SPAWN_CHANCE)
            Type = EntityType.Taxi;

        Transform Direction = Random.Range(0f, 1f) < .5f ? SpawnPositionLeft : SpawnPositionRight;

        GameObject o = PoolInstantiate(Type, Direction.position, Direction.rotation);
        if (o != null)
        {
            TransportEntityView v = o.GetComponent<TransportEntityView>();
            v.Speed = Random.Range(3f, 5f);
            return true;
        }
        return false;
    }

    private void SetupPool(EntityType Type, List<GameObject> Pool, int Amount)
    {
        GameObject prefab = GetPrefabByType(Type);
        if (prefab != null)
        {
            GameObject holder = new GameObject(Type.ToString() + " Holder");
            holder.transform.SetParent(transform);
            holder.transform.position = Vector3.zero;
            holder.transform.rotation = Quaternion.identity;

            for (int i = 0; i < Amount; i++)
            {
                GameObject o = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation) as GameObject;
                TransportEntityView objView = o.AddComponent<TransportEntityView>();
                objView.Spawner = this;
                objView.Type = Type;
                o.SetActive(false);
                o.transform.SetParent(holder.transform);
                Pool.Add(o);
                if(Type == EntityType.Car)
                    prefab = GetPrefabByType(Type);
            }
        }
    }

    public void Recicle(EntityType Type, GameObject obj)
    {
        List<GameObject> pool = GetPoolByType(Type);
        if(pool != null)
        {
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject PoolInstantiate(EntityType Type, Vector3 Position, Quaternion Rotation)
    {
        GameObject o = null;
        List<GameObject> pool = GetPoolByType(Type);
        if(pool != null && pool.Count > 0)
        {
            if (pool[0] != null)
            {
                o = pool[0];
                o.transform.position = Position;
                o.transform.rotation = Rotation;
                o.SetActive(true);
            }
            pool.RemoveAt(0);
        }
        return o;
    }

    List<GameObject> GetPoolByType(EntityType Type)
    {
        List<GameObject> pool = null;
        switch (Type)
        {
            case EntityType.Car:
                pool = CarPool;
                break;
            case EntityType.Bus:
                pool = BusPool;
                break;
            case EntityType.Taxi:
                pool = TaxiPool;
                break;
            case EntityType.Uber:
                pool = UberPool;
                break;
            case EntityType.Train:
                pool = TrainPool;
                break;
        }
        return pool;
    }

    GameObject GetPrefabByType(EntityType Type)
    {
        GameObject prefab = null;
        switch (Type)
        {
            case EntityType.Car:
                prefab = Car[Random.Range(0, Car.Length - 1)];
                break;
            case EntityType.Bus:
                prefab = Bus;
                break;
            case EntityType.Taxi:
                prefab = Taxi;
                break;
            case EntityType.Train:
                prefab = Train;
                break;
            case EntityType.Uber:
                prefab = Uber;
                break;
        }
        return prefab;
    }
}
