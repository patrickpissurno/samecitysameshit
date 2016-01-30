using UnityEngine;
using System.Collections;

public interface IGameUIService {
    bool ClockTick { get; }
    void RestartGame();
    void GoToMainMenu();
    void GoToCredits();
    void UpdateTimer(float deltaTime);
    string GetHour();
    string GetMinute();
    int GetDay();
    string GetRandomMonth();
}
