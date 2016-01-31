using UnityEngine;
using System;
using System.Collections;

public class TriggerInteractableView : MonoBehaviour {

    public Action<GameObject> onTriggerEnter;
    public new string tag;
    public new string name;

    void OnTriggerEnter(Collider col) {
        if (col.tag == tag && col.name != name) {
            if (onTriggerEnter != null) {
                onTriggerEnter(col.gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
