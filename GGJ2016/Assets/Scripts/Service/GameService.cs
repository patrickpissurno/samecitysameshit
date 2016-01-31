using UnityEngine;
using System.Collections;
using System;

public class GameService : IGameService
{
    private static GameObject playerObject = null;

    private const int speed = 5;

    private GameView gameView;

    private PlayerModel player;

    private GameObject currentGameObject;

    private GameObject camObject;

    public void SetupGameView(GameView gameView)
    {
        this.gameView = gameView;
        player = new PlayerModel();
        InputManager.instance.onClickListener += OnClick;

        camObject = GameObject.Find(ElementType.MainCamera.ToString());
    }

    public void MovePlayer()
    {
        if (currentGameObject != null)
        {
            if (currentGameObject.name.Equals(TagType.Limit))
            {
                LoadPlayer();
                playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), speed * Time.deltaTime);
                SetFixedPosition();
            }
        }
    }

    private void LoadPlayer()
    {
        if (playerObject == null)
            playerObject = GameObject.Find(ElementType.Player.ToString());
    }

    private void SetFixedPosition()
    {
        LoadPlayer();
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
                player.SetTargetPosition(new Vector3(5.5f, player.getCurrentPosition().y, 14.5f));

                Debug.Log(gameObject.tag);
                break;


            case TagType.Limit:
                MovePlayer();
                RunAnimBusStopToDefault();
                player.SetTargetPosition(clickPosition);

                Debug.Log(gameObject.tag);
                break;

            default:
                break;
        }


        player.SetCurrentPosition(playerObject.transform.position);
    }

    public void MovePlayerToBusStop()
    {
        if (currentGameObject != null)
        {
            LoadPlayer();
            playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), speed * Time.deltaTime);
            SetFixedPosition();
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
