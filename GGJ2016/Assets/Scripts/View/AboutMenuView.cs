using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AboutMenuView : MonoBehaviour {

    public Transform credits;
    private const int amountToWait = 18;
    private const float SPEED = 35;
    private IAboutMenuController AboutMenuController;

    void Start()
    {
        AboutMenuController = new AboutMenuController();
        StartCoroutine(WaitForAnimation());
    }

    void Update()
    {
        credits.Translate(Vector3.up * Time.deltaTime * SPEED);
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(amountToWait);
        AboutMenuController.AnimationEnd();
    }
}
