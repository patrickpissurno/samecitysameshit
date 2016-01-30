using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static Action<GameObject> onClickListener;

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
            OnMouseDown(new GameObject());
        }

	}

    private void OnMouseDown(GameObject obj) {
        onClickListener(obj);
    }
}
