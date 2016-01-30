using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static Action<GameObject, Vector3> onClickListener;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            #if UNITY_ANDROID
                Vector3 position = Input.GetTouch(0).position;
            #else
                Vector3 position = Input.mousePosition;
            #endif

            Ray ray = Camera.main.ScreenPointToRay(position);
            OnMouseDown(this.gameObject, ray);
        }

	}

    private void OnMouseDown(GameObject gameObject, Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            onClickListener(hit.transform.gameObject, hit.point);
        }
    }
}
