using UnityEngine;
using System.Collections;

public interface IGameService
{
    void setupGameView(GameView GameView);

    void MovePlayer();

    void MovePlayerToBusStop();

    void RunAnimCamToBusStop();

    void RunAnimBusStopToDefault();
}
