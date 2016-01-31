using UnityEngine;
using System.Collections;

public interface IGameService
{
    void SetupGameView(GameView GameView);

    void MovePlayer();

    void RunAnimCamZoomInToBusStop();

    void RunAnimCamZoomOutToBusStop();
}
