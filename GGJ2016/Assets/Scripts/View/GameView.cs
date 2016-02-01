using UnityEngine;
using System.Collections;
using System;

public class GameView : MonoBehaviour {
    private IGameService gameService;
    private IGameUIService gameUIService;

	void Start () {
        gameService = new GameService();
        gameService.SetupGameView(this);
	}

	void Update () {
        gameService.MovePlayer();
        gameService.GoWalk();

        //if (Input.GetKeyDown(KeyCode.F5))
        //    GameManager.getInstance().ChangeScene("Cena_Fired");
	}

    public void SetupUIService(IGameUIService service)
    {
        if (gameUIService == null)
            gameUIService = service;
        if(gameService != null)
            gameService.SetupGameUIService(service);
    }
}
