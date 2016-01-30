using UnityEngine;
using System.Collections;

public class GameView : MonoBehaviour {

    IGameService GameService;

	void Start () {
        GameService = new GameService();
	}

	void Update () {

	}
}
