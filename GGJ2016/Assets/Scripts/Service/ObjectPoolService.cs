using UnityEngine;
using System.Collections;

public class ObjectPoolService {

    public Transform component;
    public bool InUse = false;

    public void SetActive(bool active) {
        component.gameObject.SetActive(active);
        InUse = active;
    }

    public void ResetPosition(Vector3 position) {
        component.position = position;
    }

    public void Reset(Vector3 position, float YRotation, string name) {
        SetActive(true);
        ResetPosition(position);

        component.name = name;

        Vector3 angle = component.eulerAngles;
        angle.y = YRotation;
        component.eulerAngles = angle;
    }
}
