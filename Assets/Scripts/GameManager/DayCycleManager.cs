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
    public event Action<int> OnShopPhaseEnded;

    public int GetCurrentDay() => currentDay;

    private void Start() {
        GameManager.Instance.OnCraftingStateStarted += Handle_OnCraftingStateStarted;
        GameManager.Instance.OnShopStateStarted += Handle_OnShopStateStarted;
        GameManager.Instance.OnDungeonStateStarted += Handle_OnDungeonStateStarted;
        GameManager.Instance.OnResultStateStarted += Handle_OnResultStateStarted;
        GameManager.Instance.OnResultStateEnded += Handle_OnResultStateEnded;
    }


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
    }


    private void Handle_OnCraftingStateStarted(GameState state) {
        //throw new NotImplementedException();
    }

    public void Handle_OnShopStateStarted(GameState state) {
        OnCraftingPhaseEnded?.Invoke(currentDay);
    }

    public void Handle_OnDungeonStateStarted(GameState state) {
        OnShopPhaseEnded?.Invoke(currentDay);
        //FindObjectOfType<DungeonSimulator>().RunDungeon();
    }

    public void EndDungeonPhase() {
    }

    public void Handle_OnResultStateStarted(GameState state) {

    }

    public void Handle_OnResultStateEnded(GameState state) {
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
