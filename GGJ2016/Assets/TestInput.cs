using UnityEngine;
using System.Collections;

public class TestInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InputManager.onClickListener += OnClick;
        InputManager.onClickListener += OnClick2;
	}

    void OnDestroy() {
        InputManager.onClickListener -= OnClick;
        InputManager.onClickListener -= OnClick2;
    }

	// Update is called once per frame
	void OnClick (GameObject go) {
	}

    void OnClick2(GameObject go) {
    }
}
