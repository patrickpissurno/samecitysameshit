using UnityEngine;
using UnityEngine.UI;

public interface IGameUIService {
    bool ClockTick { get; }
    SpawnerView SpawnerView { set; }

    void RestartGame();

    void GoToMainMenu();

    void GoToCredits();

    void GoToGameOver();

    void UpdateTimer(float deltaTime);

    void UpdateGame();

    string GetHour();

    string GetMinute();

    int GetTotalMinutes();

    int GetDay();

    float GetHappiness();

    string GetRandomMonth();

    void ShowUberUI(Vector3 cameraPosition);

    void HideUberUI(Vector3 cameraPosition);

    void SetupRebuFillAmount(Image image);

    void CallToRebu(float fillAmount);
}
