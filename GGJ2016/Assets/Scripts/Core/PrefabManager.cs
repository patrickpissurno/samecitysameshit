using UnityEngine;
using System.Collections;

public class PrefabManager : MonoBehaviour {

    public Transform[] prefabs;

    public Transform LoadPrefab(string name) {
        foreach (Transform item in prefabs) {
            if (item.name == name) {
                return item;
            }
        }

        return null;
    }
}
