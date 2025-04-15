using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public static GameManager Instance { get; private set; }
    private GameState CurrentState;

    public event Action<GameState> OnCraftingStateStarted;
    public event Action<GameState> OnShopStateStarted;
    public event Action<GameState> OnDungeonStateStarted;
    public event Action<GameState> OnResultStateStarted;
    public event Action<GameState> OnResultStateEnded;

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
                break;
            case GameState.Shop:
                // Start shop phase
                OnShopStateStarted?.Invoke(CurrentState);
                break;
            case GameState.Dungeon:
                //Start
                OnDungeonStateStarted?.Invoke(CurrentState);
                break;
            case GameState.Results:
                OnResultStateStarted?.Invoke(CurrentState);
                break;
            case GameState.Pause:
                // Pause the game
                break;
        }
    }

    public void AdvanceToNextState() {
        switch (GetCurrentGameState()) {
            case GameState.Crafting:
                ChangeState(GameState.Shop);
                break;
            case GameState.Shop:
                ChangeState(GameState.Dungeon);
                break;
            case GameState.Dungeon:
                ChangeState(GameState.Results);
                break;
            case GameState.Results:
                OnResultStateEnded?.Invoke(CurrentState);
                ChangeState(GameState.Crafting);
                break;
        }
    }
}
