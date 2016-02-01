using UnityEngine;
using System.Collections;

public class PlayerModel {
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private string tag;
    private bool near;

    public PlayerModel(Vector3 currentPosition, Vector3 targetPosition, string tag, bool near)
    {
        this.currentPosition = currentPosition;
        this.targetPosition = targetPosition;
        this.tag = tag;
        this.near = near;
    }

    public PlayerModel()
    {

    }

    public void SetCurrentPosition(Vector3 currentPosition)
    {
        this.currentPosition = currentPosition;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public void SetTag(string tag)
    {
        this.tag = tag;
    }

    public void SetProximity(bool near)
    {
        this.near = near;
    }

    public Vector3 getCurrentPosition()
    {
        return currentPosition;
    }

    public Vector3 getTargetPosition()
    {
        return targetPosition;
    }

    public string getTag() { return tag; }

    public bool isNear() { return near; }
}
