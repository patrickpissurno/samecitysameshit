using UnityEngine;
using System.Collections;
using System;

public class GameService : IGameService
{
    GameView gameView;

    public void setupGameView(GameView gameView)
    {
        this.gameView = gameView;
        InputManager.onClickListener += OnClickMove;
    }

    public void MovePlayer()
    {

    }

    void OnClickMove(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        MovePlayer();
    }
}
