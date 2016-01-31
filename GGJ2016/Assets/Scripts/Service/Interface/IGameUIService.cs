using UnityEngine;
using System.Collections;

public interface IGameUIService {
    bool ClockTick { get; }

    void RestartGame();

    void GoToMainMenu();

    void GoToCredits();

    void GoToGameOver();

    void UpdateTimer(float deltaTime);

    void UpdateGame();

    string GetHour();

    string GetMinute();

    int GetDay();

    float GetHappiness();

    string GetRandomMonth();

    void ShowUberUI(Vector3 cameraPosition);

    void HideUberUI(Vector3 cameraPosition);

    void SetupRebuFillAmount(GameObject gameObject);

    void callToRebu(float fillAmount);
}
