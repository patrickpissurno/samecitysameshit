using UnityEngine;
using System.Collections;

public class MovementBehaviorView : IInteractableView {

    public float speed;
    public Vector3 target;

	void Start () {
        Vector4 bounds = CameraHelper.GetBounds(GameManager.getBean<Camera>());
        transform.position = new Vector3(bounds.z , transform.position.y, transform.position.z);
        target = new Vector3(bounds.x, transform.position.y, transform.position.z);
	}
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
	}

    public void SetNewTargetPosition(Vector3 newTarget) {
        target = newTarget;
    }

    public override void OnClick() {

    }
}
