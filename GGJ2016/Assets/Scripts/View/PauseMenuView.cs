using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuView : MonoBehaviour, IPauseMenuView {
    private IPauseMenuService Service;
	void Start () {
        Service = new PauseMenuService();
        gameObject.SetActive(false);
	}

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
