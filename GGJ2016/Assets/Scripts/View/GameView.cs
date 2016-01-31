using UnityEngine;
using System.Collections;
using System;

public class GameView : MonoBehaviour {
    IGameService gameService;

	void Start () {
        gameService = new GameService();
        gameService.SetupGameView(this);
	}

	void Update () {
        gameService.MovePlayer();
        gameService.MovePlayerToBusStop();
	}
}
