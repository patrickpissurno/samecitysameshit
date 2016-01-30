using UnityEngine;
using System.Collections;
using System;

public class GameService : IGameService
{
    private static readonly int speed = 10;

    GameView gameView;

    public void setupGameView(GameView gameView)
    {
        this.gameView = gameView;
        InputManager.onClickListener += OnClickMove;
    }

    public void MovePlayer(GameObject gameObject, Vector3 target)
    {
        if (gameObject.name.Equals(ElementType.Limit.ToString()))
        {
            gameView.MovePlayer(playerObject.transform.position, target, speed);
        }        
    }

    void OnClickMove(GameObject gameObject, Vector3 clickPosition)
    {
            MovePlayer(gameObject, clickPosition);
    }
}
