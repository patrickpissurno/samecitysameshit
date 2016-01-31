using UnityEngine;
using System.Collections;

public interface IGameUIService {
    bool ClockTick { get; }
    void RestartGame();
    void GoToMainMenu();
    void GoToCredits();
    void UpdateTimer(float deltaTime);
    void UpdateHapiness();
    string GetHour();
    string GetMinute();
    int GetDay();
    float GetHappiness();
    string GetRandomMonth();
}
