using UnityEngine;
using System.Collections;

public interface IGameService
{
    void setupGameView(GameView GameView);

    void MovePlayer(GameObject gameObject, Vector3 target);
}
