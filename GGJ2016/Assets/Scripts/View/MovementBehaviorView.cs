using UnityEngine;
using System.Collections;

public class MovementBehaviorView : IInteractableView {

    public float speed;

    public Vector3 target;
    public float targetSpeed;

    private const float breakSpeed = 5f;
    private TriggerInteractableView trigger = null;

    public ObjectPoolService poolComponent;

    private bool canDisable = false;

    public void Init(float bound, float speed) {
        target = new Vector3(bound, transform.position.y, transform.position.z);
        
        this.speed = speed;
        this.targetSpeed = speed;

        if(trigger == false) {
            trigger = GetComponentInChildren<TriggerInteractableView>();
            trigger.tag = "Vehicle";
            trigger.onTriggerEnter += VehicleInFront;
        } else {
            trigger.gameObject.SetActive(true);
        }

        trigger.name = transform.name;

        GameManager.WaitTime(2, () => {
            canDisable = true;
        });
    }
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
        speed = Mathf.Lerp(speed, targetSpeed, Time.deltaTime * breakSpeed);

        if (!GetComponent<Renderer>().isVisible && canDisable) {
            canDisable = false;
            poolComponent.SetActive(false);
        }
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
