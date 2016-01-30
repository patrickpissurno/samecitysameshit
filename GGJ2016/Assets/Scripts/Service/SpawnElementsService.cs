using UnityEngine;
using System;
using System.Collections;

public class SpawnElementsService {

    private SpawnElementsModel previousSpawnElement;
    
    private static SpawnElementsService instance;
    private SpawnElementsView spawnReference;

    public Action<SpawnElementsModel> OnElementGenerated;

    public bool Break;

    public static SpawnElementsService Instance{
        get { if (instance == null) instance = new SpawnElementsService(); return instance; }
    }

    private SpawnElementsService() {

    }

    public void Initialize(SpawnElementsView reference) {
        spawnReference = reference;
        WaitAndSearch();
    }

    public void WaitAndSearch() {
        float wait = UnityEngine.Random.Range(2.0f, 5.0f);
        GameManager.WaitTime(wait, CreateNewElement);
    }

    public void CreateNewElement() {

        if (Break) {
            return;
        }

        StreetSide side = UnityEngine.Random.Range(0, 6) > 2 ? StreetSide.LeftSide : StreetSide.RightSide;
        float speed = GenerateSpeed(side);
        SpawnType type = GenerateObject();
        
        SpawnElementsModel component = new SpawnElementsModel {
            side = side,
            speed = speed,
            element = type
        };


        previousSpawnElement = component;

        if (OnElementGenerated != null) {
            OnElementGenerated(component);
        }

        WaitAndSearch();
    }

    private float GenerateSpeed(StreetSide side) {
        if (previousSpawnElement != null && previousSpawnElement.speed < 0.25f && previousSpawnElement.side == side) {
            return UnityEngine.Random.Range(0.15f, 0.35f);
        }

        return UnityEngine.Random.Range(0.15f, 0.55f);
    }

    private SpawnType GenerateObject() {
        float percentage = UnityEngine.Random.Range(0, SumPercentages());
        float[] elements = PercentageAsArray();

        float initial = 0;

        for (int i = 0; i < elements.Length; i++) {
            float current = initial + elements[i];
            
            if (percentage > initial && percentage < current) {
                return GetTypeFromInt(i);
            }

            initial = current;
        }

        return GetTypeFromInt(elements.Length - 1);
    }

    public float SumPercentages() {
        float[] elements = PercentageAsArray();

        float sum = 0.0f;

        for (int i = 0; i < elements.Length; i++) {
			 sum += elements[i];
		}

        return sum;
    }

    
    public float[] PercentageAsArray() {
        return new float[] {
            spawnReference.BusPercentage,
            spawnReference.CarPercentage,
            spawnReference.TaxiPercentage,
            spawnReference.UberPercentage
        };
    }

    public SpawnType GetTypeFromInt(int index) {
        switch (index) {
            case 0:
                return SpawnType.Bus;
            case 1:
                return SpawnType.Car;
            case 2:
                return SpawnType.Taxi;
            default:
                return SpawnType.Uber;
        }
    }
}
