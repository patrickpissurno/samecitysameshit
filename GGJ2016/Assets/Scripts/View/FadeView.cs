using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeView : MonoBehaviour {

    public float speed;

    private Image fadePanel;

    private float current,target = 0;
    private bool running;

    
	public void PlayForward() {
        current = 0;
        target = 1;
        Begin();
    }

    public void PlayReverse() {
        current = 1;
        target = 0;
        Begin();
    }

    private void Begin() {

        if(fadePanel == null) {
            fadePanel = GetComponent<Image>();
        }
        fadePanel.enabled = true;
        running = true;
        UpdateAlpha(current);
    }

    private void End() {
        running = false;
        UpdateAlpha(target);
        
    }
    void Update () {
        if (running) {
            current = Mathf.Lerp(current, target, speed * Time.deltaTime);
            UpdateAlpha(current);
            if(target == 1 && current + 0.03f >= target) {
                End();
            } else if(target == 0 && current - 0.03f <= target) {
                End();
            }
        }
    }

    private void UpdateAlpha(float value) {
        Color c = fadePanel.color;
        c.a = value;
        fadePanel.color = c;
        if (value <= 0)
            gameObject.SetActive(false);
    }
}
