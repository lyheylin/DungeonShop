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
    [SerializeField] private GameObject craftingUICanvas;
    [SerializeField] private GameObject shopUICanvas;
    [SerializeField] private AdventurerViewerUI adventurerViewerUI;

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

        GameManager.Instance.OnCraftingStateStarted += Handle_OnCraftingStateStarted;
        GameManager.Instance.OnShopStateStarted += Handle_OnShopStateStarted;
        GameManager.Instance.OnDungeonStateStarted += Handle_OnDungeonStateStarted;
        SaveManager.Instance.OnDataLoaded += Handle_OnDataLoaded;
        adventurerViewerUI.Initialize();
    }

    //handlers
    private void Handle_OnDataLoaded(GameSaveData data) {
        RefreshUIForCurrentGameState();
    }

    private void Handle_OnCraftingStateStarted(GameState state) {
        DisableMainPanel();

        craftingUICanvas.SetActive(true);
    }
    private void Handle_OnShopStateStarted(GameState obj) {
        DisableMainPanel();

        shopUICanvas.SetActive(true);
    }
    private void Handle_OnDungeonStateStarted(GameState state) {
        DisableMainPanel();
    }

    //
    private void AdvanceToNextPhase() {
        GameManager.Instance.AdvanceToNextState();
    }

    public void DisableMainPanel() {
        craftingUICanvas.SetActive(false);
        shopUICanvas.SetActive(false);
    }
    public void RefreshUIForCurrentGameState() {
        GameState state = GameManager.Instance.GetCurrentGameState();

        // Disable all panels first
        DisableMainPanel();
        adventurerViewerUI.gameObject.SetActive(false);

        // Activate the correct one
        switch (state) {
            case GameState.Crafting:
                craftingUICanvas.SetActive(true);
                adventurerViewerUI.gameObject.SetActive(true);
                break;
            case GameState.Shop:
                shopUICanvas.SetActive(true);
                adventurerViewerUI.gameObject.SetActive(true);
                break;
                // Add other cases as needed
        }

    }
}
