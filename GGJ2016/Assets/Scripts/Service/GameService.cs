using UnityEngine;
using System.Collections;
using System;

public class GameService : IGameService
{
    private static readonly GameObject playerObject = GameObject.Find(ElementType.Player.ToString());

    private static readonly float speed = 1.5f;

    private GameView gameView;

    private PlayerModel player;

    private GameObject currentGameObject;

    private GameObject camObject;

    private Animation anim;

    public void setupGameView(GameView gameView)
    {
        this.gameView = gameView;
        player = new PlayerModel();
        InputManager.onClickListener += OnClick;

        camObject = GameObject.Find(ElementType.MainCamera.ToString());
        anim = playerObject.GetComponent<Animation>();
    }

    public void MovePlayer()
    {
        if (currentGameObject != null)
        {
            if (currentGameObject.name.Equals(TagType.Limit))
            {
                Vector3 forward = player.getTargetPosition() - playerObject.transform.position;
                forward.y = 0;

                if (Vector3.Distance(Vector3.zero, forward) > 0.2f && player.getTargetPosition() != Vector3.zero) {
                    Quaternion direction = Quaternion.LookRotation(forward);
                    playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, direction, speed * 4 * Time.deltaTime);
                }

                if (Vector3.Distance(Vector3.zero, forward) > 0.1f) {
                    if (!anim.IsPlaying("Walk")) {
                        anim.CrossFade("Walk", 0.3f);
                    }
                } else {
                    if (!anim.IsPlaying("Idle")) {
                        anim.CrossFade("Idle", 0.3f);
                    }
                }


                playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), speed * Time.deltaTime);
                setFixedPosition();
            }
        }
    }

    private void setFixedPosition()
    {
        playerObject.transform.position = new Vector3(playerObject.transform.position.x, 0.16f, playerObject.transform.position.z);
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
