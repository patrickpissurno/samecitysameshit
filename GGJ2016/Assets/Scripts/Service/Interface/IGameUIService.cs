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
}
