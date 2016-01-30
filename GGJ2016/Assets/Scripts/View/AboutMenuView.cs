using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AboutMenuView : MonoBehaviour {

    public Transform credits;
    private const int amountToWait = 18;
    private const float SPEED = 35;
    private IAboutMenuService AboutMenuController;

    void Start()
    {
        AboutMenuController = new AboutMenuService();
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
