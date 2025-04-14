using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour {

    [SerializeField] private InventoryPanelUI inventoryPanelUI;
    [SerializeField] private SellingSlotsDisplayUI sellingSlotsDisplayUI;

    private void OnEnable() {
        inventoryPanelUI.RefreshInventory();
        sellingSlotsDisplayUI.RefreshShopSlots();
        if (GameManager.Instance != null) {
            GameManager.Instance.OnCraftingStateStarted += Handle_OnCraftingStateStarted;
            DayCycleManager.Instance.OnCraftingPhaseEnded += Handle_OnCraftingStateEnded;
        }
    }

    private void OnDisable() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnCraftingStateStarted -= Handle_OnCraftingStateStarted;
            DayCycleManager.Instance.OnCraftingPhaseEnded -= Handle_OnCraftingStateEnded;
        }
    }

    private void Handle_OnCraftingStateStarted(GameState state) {
        inventoryPanelUI.RefreshInventory();
        sellingSlotsDisplayUI.RefreshShopSlots();
    }

    private void Handle_OnCraftingStateEnded(int day) {
        sellingSlotsDisplayUI.MoveItemsToShop();
    }
}
