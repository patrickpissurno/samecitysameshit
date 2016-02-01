using UnityEngine;
using System.Collections;

public class ActiveObjectView : MonoBehaviour {

    public GameObject obj;

    public void Appear() {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
