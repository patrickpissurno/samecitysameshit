using UnityEngine;
using System.Collections;

public class CameraHelper {

    public static Vector2 getMainGameViewSize() {
        if (Application.isEditor) {
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
            Debug.Log(Res);
            return (Vector2)Res;
        } else {
            return new Vector2(Screen.width, Screen.height);
        }
    }

    public static Vector4 GetBounds(Camera cam) {
        Vector3 minBounds = GetWorldPositionOnPlane(Vector3.zero, 0);
        Vector3 maxBounds = GetWorldPositionOnPlane(new Vector3(getMainGameViewSize().x, getMainGameViewSize().y, 0), 0);
        Debug.Log(maxBounds);
        return new Vector4(minBounds.x, minBounds.y, maxBounds.x, maxBounds.y);
    }

    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
