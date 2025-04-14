using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour {
    public static DayCycleManager Instance { get; private set; }

    private int currentDay = 1;
    public event Action<int> OnDayStarted;
    public event Action<int> OnDayEnded;
    public event Action<int> OnCraftingPhaseEnded;

    public int GetCurrentDay() => currentDay;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartDay() {
        Debug.Log($"Day {currentDay} started.");
        OnDayStarted?.Invoke(currentDay);

        GameManager.Instance.ChangeState(GameState.Crafting);
    }

    public void EndCraftingPhase() {
        OnCraftingPhaseEnded?.Invoke(currentDay);
        GameManager.Instance.ChangeState(GameState.Shop);
    }

    public void EndShopPhase() {
        GameManager.Instance.ChangeState(GameState.Dungeon);
    }

    public void StartDungeonPhase() {
        FindObjectOfType<DungeonSimulator>().RunDungeon();
    }

    public void EndDungeonPhase() {
        GameManager.Instance.ChangeState(GameState.Results);
    }

    public void FinishResultsAndEndDay() {
        Debug.Log($"Day {currentDay} ended.");
        OnDayEnded?.Invoke(currentDay);

        currentDay++;
        StartDay();
    }

    public TimeSaveData GetSaveData() {
        return new TimeSaveData {
            currentDay = currentDay,
            currentGameState = GameManager.Instance.GetCurrentGameState()
        };
    }

    public void LoadFromSaveData(TimeSaveData data) {
        currentDay = data.currentDay;
        GameManager.Instance.ChangeState(data.currentGameState);
    }
}
