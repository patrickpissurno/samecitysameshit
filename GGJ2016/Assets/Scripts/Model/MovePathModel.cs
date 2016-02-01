using UnityEngine;
using System.Collections;

[System.Serializable]
public class MovePathModel {

    public float waitForBeggining;
    public float speed;
    public float waitForStop;

    public bool useRotation;

    public string playAnimation;
<<<<<<< HEAD
=======
    public bool useCrossfade = true;
    
    public Transform targetForStop;
    public bool stopOnEnd;

>>>>>>> refs/remotes/origin/develop
    public string changeSceneOnEnd;

    public GameObject target;
    public string callMethodOnEnd;

    public Transform[] paths;
}
