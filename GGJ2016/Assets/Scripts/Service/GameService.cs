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
    private Animation anim;

    private string currentTag;
    private GameUIService gameUIService;

    public void SetupGameView(GameView gameView)
    {
        LoadPlayer();

        this.gameView = gameView;
        player = new PlayerModel();
        InputManager.instance.onClickListener += OnClick;

        camObject = GameObject.Find(ElementType.MainCamera.ToString());


        if (anim == null)
        {
            anim = playerObject.GetComponent<Animation>();
        }
    }

    private void LoadPlayer()
    {
        if (playerObject == null)
            playerObject = GameObject.Find(ElementType.Player.ToString());
    }

    private void RotateAndAnim()
    {
        Vector3 forward = player.getTargetPosition() - playerObject.transform.position;
        forward.y = 0;

        if (Vector3.Distance(Vector3.zero, forward) > 0.2f && player.getTargetPosition() != Vector3.zero)
        {
            Quaternion direction = Quaternion.LookRotation(forward);
            playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, direction, speed * 4 * Time.deltaTime);
        }

        if (Vector3.Distance(Vector3.zero, forward) > 0.1f)
        {
            if (!anim.IsPlaying("Walk"))
            {
                anim.CrossFade("Walk", 0.3f);
            }
        }
        else {
            if (!anim.IsPlaying("Idle"))
            {
                anim.CrossFade("Idle", 0.3f);
            }
        }
    }

    private void SetFixedPosition()
    {
        LoadPlayer();
        playerObject.transform.position = new Vector3(playerObject.transform.position.x, 0.16f, playerObject.transform.position.z);
    }

    void OnClick(GameObject gameObject, Vector3 clickPosition)
    {
        currentGameObject = gameObject;
        currentTag = gameObject.tag;

        initRoutine(gameObject.tag);

        switch (gameObject.tag)
        {
            case TagType.BusStop:
                player.SetTag(gameObject.tag);
                MovePlayer();

                RunAnimCamZoomInToBusStop();
                //MoveToAnotherPoint();

                player.SetTargetPosition(new Vector3(5.5f, player.getCurrentPosition().y, 14.5f));
                break;


            case TagType.Limit:
                HandleLimitAnim();
                player.SetTag(gameObject.tag);

                MovePlayer();
                player.SetTargetPosition(clickPosition);
                break;

            case TagType.Bus:
                break;

            case TagType.Taxi:

                break;

            case TagType.Rebu:

                break;

            case TagType.Bike:
                break;

            case TagType.Walk:
                player.SetTag(gameObject.tag);

                MovePlayer();
                MoveToAnotherPoint();
                player.SetTargetPosition(new Vector3(20.81f, player.getCurrentPosition().y, 14.5f));
                break;

            case TagType.Garage:
                MovePlayer();
                player.SetTag(gameObject.tag);

                RunAnimOpenGarage(gameObject);
                RunAnimCamZoomInToGarage();
                //MoveToAnotherPoint();

                player.SetTargetPosition(new Vector3(-1.5f, player.getCurrentPosition().y, 14.5f));
                break;

            default:
                break;
        }
        player.SetCurrentPosition(playerObject.transform.position);
    }

    public void MovePlayer()
    {
        if (currentGameObject != null)
        {
            if (currentGameObject.tag == player.getTag())
            {
                LoadPlayer();
                RotateAndAnim();

                playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, player.getTargetPosition(), speed * Time.deltaTime);
                SetFixedPosition();
            }
            return;
        }
    }

    public void RunAnimCamZoomInToBusStop()
    {
        camObject.GetComponent<Animator>().SetBool("canZoom", true);
        player.SetProximity(true);
        Debug.Log("is near bus stop");
    }

    public void RunAnimCamZoomOutToBusStop()
    {
        camObject.GetComponent<Animator>().SetBool("canZoom", false);
        player.SetProximity(false);
        Debug.Log("is not near bus stop");
    }

    public void RunAnimOpenGarage(GameObject gameobject)
    {
        gameobject.GetComponent<Animator>().SetBool("canOpen", true);
    }

    public void RunAnimCamZoomInToGarage()
    {
        camObject.GetComponent<Animator>().SetBool("canZoomToGarage", true);
        player.SetProximity(true);
        Debug.Log("is near garage");
    }

    public void RunAnimCamZoomOutToGarage()
    {
        camObject.GetComponent<Animator>().SetBool("canZoomToGarage", false);
        player.SetProximity(false);
        Debug.Log("is not near garage");
    }

    public void RunAnimCamMoveFromStopBusToGarage(bool isNear)
    {
        if (isNear)
        {
            camObject.GetComponent<Animator>().SetInteger("moveToAnotherPoint", MoveType.ToGarage);
            Debug.Log("RunAnimCamMoveFromStopBusToGarage");
        }
    }

    public void RunAnimCamMoveFromGarageToStopBus(bool isNear)
    {
        if (isNear)
        {
            camObject.GetComponent<Animator>().SetInteger("moveToAnotherPoint", MoveType.ToStopBus);
            Debug.Log("RunAnimCamMoveFromGarageToStopBus");
        }
    }

    public void RunAnimCamOutGarage()
    {
        camObject.GetComponent<Animator>().SetInteger("moveToAnotherPoint", MoveType.OutGarage);
    }

    public void RunAnimCamOutStopBus()
    {
        camObject.GetComponent<Animator>().SetInteger("moveToAnotherPoint", MoveType.OutStopBus);
    }

    public void RunAnimCamWalk()
    {
        camObject.GetComponent<Animator>().SetBool("canWalk",true);
    }

    private void HandleLimitAnim()
    {
        switch (player.getTag())
        {
            case TagType.BusStop:
                RunAnimCamZoomOutToBusStop();
                break;

            case TagType.Garage:
                RunAnimCamZoomOutToGarage();
                break;

            default:
                break;
        }
    }

    private void MoveToAnotherPoint()
    {
        Debug.Log("MoveToAnotherPoint is near");
        switch (player.getTag())
        {
            case TagType.Walk:
                RunAnimCamWalk();
                break;

            case TagType.BusStop:
                RunAnimCamMoveFromStopBusToGarage(player.isNear());
                break;

            case TagType.Garage:
                RunAnimCamMoveFromGarageToStopBus(player.isNear());
                break;

            case TagType.Limit:
                RunAnimCamOutGarage();
                break;

            default:
                break;
        }
    }

    public void GoWalk()
    {
        if (player.getTag() == TagType.Walk)
        {
            if (playerObject.transform.position.x >= 15)
            {
                GameManager.getInstance().ChangeScene(new SceneRoutineModel().walk[UnityEngine.Random.Range(0, 3)]);
            }
        }

    }

    private void initRoutine(string tag)
    {
        SceneRoutineModel sceneRoutine = new SceneRoutineModel();
        int num = UnityEngine.Random.Range(0, 3);
        TimeModel model = GameUIService.UIService.GetTimeModel();
        int h, m;
        switch (tag)
        {
            case TagType.Walk:
                break;

            case TagType.Taxi:
                GameManager.getInstance().ChangeScene(sceneRoutine.taxi[num]);
                h = 1;
                m = 10;
                if (num != 2)
                    m += 30;
                model.Hour += h;
                model.Minute += m;
                break;

            case TagType.Rebu:
                //GameManager.getInstance().ChangeScene(sceneRoutine.walk[UnityEngine.Random.Range(0, 3)]);
                break;

            case TagType.Bus:
                GameManager.getInstance().ChangeScene(sceneRoutine.bus[num]);
                h = 1;
                m = 30;
                if (num != 2)
                    m += 30;
                model.Hour += h;
                model.Minute += m;
                break;

            case TagType.Bike:
                GameManager.getInstance().ChangeScene(sceneRoutine.bike[num]);
                h = 2;
                m = 0;
                if (num != 2)
                    m += 30;
                model.Hour += h;
                model.Minute += m;
                break;

            case TagType.Train:
                GameManager.getInstance().ChangeScene(sceneRoutine.train[num]);
                h = 1;
                m = 0;
                if (num != 2)
                    m += 30;
                model.Hour += h;
                model.Minute += m;
                break;

            case TagType.Car:
                GameManager.getInstance().ChangeScene(sceneRoutine.car[num]);
                h = 1;
                m = 0;
                if (num != 2)
                    h += 1;
                model.Hour += h;
                model.Minute += m;
                break;

            default:
                break;
        }
    }

    //public void RunAnimCloseGarage()
    //{
    //    GameObject.Find(TagType.Garage.ToString()).GetComponent<Animator>().SetBool("canOpen", false);
    //}
}
