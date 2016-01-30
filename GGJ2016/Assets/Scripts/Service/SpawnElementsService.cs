using UnityEngine;
using System.Collections;

public class SpawnElements {

    public static void Initialize() {
        //GameManager.WaitTime();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
