using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    public Action<GameObject, Vector3> onClickListener;

    public Action<Vector3> onCameraClickPressedListener;

    public Action<Vector3> onCameraClickUpListener;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(setupPlataformPosition());
            OnClickDown(this.gameObject, ray);
        }

        else if (Input.GetMouseButton(0))
        {
            OnCameraClickPressed();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            OnCameraClickUp();
        }
    }


    private Vector3 setupPlataformPosition()
    {
        #if UNITY_ANDROID
            Vector3 position = Input.GetTouch(0).position;
        #else
            Vector3 position = Input.mousePosition;
        #endif

        return position;
    }

    private void OnClickDown(GameObject gameObject, Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
            onClickListener(hit.transform.gameObject, hit.point);
    }

    private void OnCameraClickPressed()
    {
        onCameraClickPressedListener(setupPlaneVector());
    }

    private void OnCameraClickUp()
    {
        onCameraClickUpListener(setupPlaneVector());
    }

    private Vector3 setupPlaneVector()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
        float distance;
        xy.Raycast(ray, out distance);

        return ray.GetPoint(distance);
    }
}
