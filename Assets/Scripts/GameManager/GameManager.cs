using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public static GameManager Instance { get; private set; }
    private GameState CurrentState;

    public event Action<GameState> OnCraftingStateStarted;
    public event Action<GameState> OnShopStateStarted;

    public GameState GetCurrentGameState() => CurrentState;
    private void Awake() {

        //Singleton
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState) {
        CurrentState = newState;
        Debug.Log($"Game state changed to: {newState}");

        switch (newState) {
            case GameState.MainMenu:
                // Show main menu UI
                break;
            case GameState.Crafting:
                OnCraftingStateStarted?.Invoke(CurrentState);
                // Load crafting UI and scene elements
                break;
            case GameState.Shop:
                // Start shop phase
                OnShopStateStarted?.Invoke(CurrentState);
                break;
            case GameState.Dungeon:
                DayCycleManager.Instance.StartDungeonPhase();
                break;
            case GameState.Results:
                // Display results and loot
                break;
            case GameState.Pause:
                // Pause the game
                break;
        }
    }
}
