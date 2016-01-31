using UnityEngine;
using System.Collections;

public class FinalSceneView : MonoBehaviour {

    public GameObject[] disableObjects;
    public GameObject[] enableObjects;

    public void ChangeSceneAndModels() {
        foreach (GameObject item in disableObjects) {
            item.SetActive(false);
        }

        foreach (GameObject item in enableObjects) {
            item.SetActive(true);
        }

        GameManager.WaitTime(3.5f, () => {
            GameManager.getInstance().ChangeScene("About");
        });
    }
}
