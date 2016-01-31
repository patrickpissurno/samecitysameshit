using UnityEngine;
using System.Collections;

public class PlayerModel {
    private Vector3 currentPosition;
    private Vector3 targetPosition;

    public PlayerModel(Vector3 currentPosition, Vector3 targetPosition)
    {
        this.currentPosition = currentPosition;
        this.targetPosition = targetPosition;
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

    public Vector3 getCurrentPosition()
    {
        return currentPosition;
    }

    public Vector3 getTargetPosition()
    {
        return targetPosition;
    }
}
