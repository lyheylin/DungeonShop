using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button craftingButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button dungeonButton;
    [SerializeField] private Button resultsButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button nextPhaseButton;


    private void Start() {
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.MainMenu));
        craftingButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Crafting));
        shopButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Shop));
        dungeonButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Dungeon));
        resultsButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Results));
        pauseButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Pause));
        nextPhaseButton.onClick.AddListener(AdvanceToNextPhase);
    }

    private void AdvanceToNextPhase() {
        switch (GameManager.Instance.CurrentState) {
            case GameState.Crafting:
                DayCycleManager.Instance.EndCraftingPhase();
                break;
            case GameState.Shop:
                DayCycleManager.Instance.EndShopPhase();
                break;
            case GameState.Dungeon:
                DayCycleManager.Instance.EndDungeonPhase();
                break;
            case GameState.Results:
                DayCycleManager.Instance.FinishResultsAndEndDay();
                break;
        }
    }
}
