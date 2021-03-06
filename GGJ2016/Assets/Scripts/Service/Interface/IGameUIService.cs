﻿using UnityEngine;
using UnityEngine.UI;

public interface IGameUIService {
    bool ClockTick { get; }
    SpawnerView SpawnerView { set; }

    void ReloadScene();

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

    string GetMonth();

    void ShowUberUI(Vector3 cameraPosition);

    void HideUberUI(Vector3 cameraPosition);

    void SetupUberFillAmount(Image image);

    void CallUber(float fillAmount);
}
