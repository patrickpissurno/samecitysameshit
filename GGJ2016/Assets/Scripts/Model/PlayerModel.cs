using UnityEngine;
using System.Collections;

public class PlayerModel {
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private string tag;

    public PlayerModel(Vector3 currentPosition, Vector3 targetPosition, string tag)
    {
        this.currentPosition = currentPosition;
        this.targetPosition = targetPosition;
        this.tag = tag;
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

    public Vector3 getCurrentPosition()
    {
        return currentPosition;
    }

    public Vector3 getTargetPosition()
    {
        return targetPosition;
    }

    public string getTag() { return tag; }
}
