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
    [SerializeField] private Button nextPhaseButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private GameObject inventoryCanvas;

    private void Start() {
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.MainMenu));
        craftingButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Crafting));
        shopButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Shop));
        dungeonButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Dungeon));
        resultsButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Results));
        nextPhaseButton.onClick.AddListener(AdvanceToNextPhase);
        saveButton.onClick.AddListener(() => SaveManager.Instance.SaveGame());
        loadButton.onClick.AddListener(() => SaveManager.Instance.LoadGame());
        inventoryButton.onClick.AddListener(() => Inventory.Instance.ListItems());
    }

    private void AdvanceToNextPhase() {
        switch (GameManager.Instance.GetCurrentGameState()) {
            case GameState.Crafting:
                DayCycleManager.Instance.EndCraftingPhase();
                inventoryCanvas.SetActive(false);
                break;
            case GameState.Shop:
                DayCycleManager.Instance.EndShopPhase();
                break;
            case GameState.Dungeon:
                DayCycleManager.Instance.EndDungeonPhase();
                break;
            case GameState.Results:
                inventoryCanvas.SetActive(true);
                DayCycleManager.Instance.FinishResultsAndEndDay();
                break;
        }
    }
}
