using UnityEngine;
using System.Collections;

public class MovementBehaviorView : IInteractableView {

    public float speed;

    public Vector3 target;
    public float targetSpeed;

    private const float breakSpeed = 5f;

    public void Init(float bound, float speed) {
        target = new Vector3(bound, transform.position.y, transform.position.z);
        
        this.speed = speed;
        this.targetSpeed = speed;

        TriggerInteractableView trigger = GetComponentInChildren<TriggerInteractableView>();
        trigger.tag = "Vehicle";
        trigger.name = transform.name;
        trigger.onTriggerEnter += VehicleInFront;
    }
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * breakSpeed);
	}

    public void SetNewTargetPosition(Vector3 newTarget) {
        target = newTarget;
    }

    public void SetNewSpeed(float newSpeed) {
        targetSpeed = newSpeed;
    }


    public override void OnClick() {

    }

    public void VehicleInFront(GameObject obj){
        SetNewSpeed(obj.GetComponent<MovementBehaviorView>().speed -0.01f);

    }

}
