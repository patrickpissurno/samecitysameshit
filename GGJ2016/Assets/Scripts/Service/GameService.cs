using UnityEngine;
using System.Collections;
using System;

public class GameService : IGameService
{
    private static readonly GameObject playerObject = GameObject.Find(ElementType.Player.ToString());

    private static readonly int speed = 5;

    private GameView gameView;

    private PlayerModel player;

    private GameObject currentGameObject;

    public void setupGameView(GameView gameView)
    {
        this.gameView = gameView;
        player = new PlayerModel();
        InputManager.onClickListener += OnClickMove; 

    }

    public void MovePlayer()
    {
        if (currentGameObject != null)
        {
            if (currentGameObject.name.Equals(ElementType.Limit.ToString()))
            {
                float step = speed * Time.deltaTime;

                playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), step);
                setFixedPosition();
            }
        }
    }

    private void setFixedPosition()
    {
        playerObject.transform.position = new Vector3(playerObject.transform.position.x, 0.7f, playerObject.transform.position.z);
    }

    void OnClickMove(GameObject gameObject, Vector3 clickPosition)
    {
        MovePlayer();
        currentGameObject = gameObject;
        player.setCurrentPosition(playerObject.transform.position);
        player.setTargetPosition(clickPosition);
    }
}
