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

    private GameObject camObject;

    public void setupGameView(GameView gameView)
    {
        this.gameView = gameView;
        player = new PlayerModel();
        InputManager.onClickListener += OnClick;

        camObject = GameObject.Find(ElementType.MainCamera.ToString());
    }

    public void MovePlayer()
    {
        if (currentGameObject != null)
        {
            if (currentGameObject.name.Equals(TagType.Limit))
            {
                playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), speed * Time.deltaTime);
                setFixedPosition();
            }
        }
    }

    private void setFixedPosition()
    {
        playerObject.transform.position = new Vector3(playerObject.transform.position.x, 0.7f, playerObject.transform.position.z);
    }

    void OnClick(GameObject gameObject, Vector3 clickPosition)
    {
        currentGameObject = gameObject;

        switch (gameObject.tag)
        {
            case TagType.BusStop:
                MovePlayerToBusStop();
                RunAnimCamToBusStop();
                player.setTargetPosition(new Vector3(5.5f, player.getCurrentPosition().y, 14.5f));

                Debug.Log(gameObject.tag);
                break;


            case TagType.Limit:
                MovePlayer();
                RunAnimBusStopToDefault();
                player.setTargetPosition(clickPosition);

                Debug.Log(gameObject.tag);
                break;

            default:
                break;
        }


        player.setCurrentPosition(playerObject.transform.position);
    }

    public void MovePlayerToBusStop()
    {
        if (currentGameObject != null)
        {
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), speed * Time.deltaTime);
            setFixedPosition();
        }
    }

    public void RunAnimCamToBusStop()
    {
        camObject.GetComponent<Animator>().SetBool("canZoom", true);
    }

    public void RunAnimBusStopToDefault()
    {
        camObject.GetComponent<Animator>().SetBool("canZoom", false);
    }
}
