using UnityEngine;
using System.Collections;

public interface IGameService
{
    void SetupGameView(GameView gameView);

    void SetupGameUIService(IGameUIService service);

    void MovePlayer();

    void RunAnimCamZoomInToBusStop();

    void RunAnimCamZoomOutToBusStop();

    void GoWalk();
}
