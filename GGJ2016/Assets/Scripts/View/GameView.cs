using UnityEngine;
using System.Collections;
using System;

public class GameView : MonoBehaviour {
    IGameService gameService;

	void Start () {
        gameService = new GameService();
        gameService.setupGameView(this);
	}

	void Update () {

	}

    void MovePlayer(Vector3 position, Vector3 target, int speed)
    {
        ObjectType.playerObject.transform.position = Vector3.Lerp(ObjectType.playerObject.transform.position, target, Time.deltaTime * speed);
    }
}
