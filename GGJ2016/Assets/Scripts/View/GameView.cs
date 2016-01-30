using UnityEngine;
using System.Collections;

public class GameView : MonoBehaviour {

    IGameService gameService;

	void Start () {
        gameService = new GameService();
	}

	void Update () {

	}
}
