using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour {

    [SerializeField] private InventoryPanelUI inventoryPanelUI;
    [SerializeField] private ShopDisplayUI shopDisplayUI;

    private void OnEnable() {
        inventoryPanelUI.RefreshInventory();
        shopDisplayUI.RefreshShopSlots();
        if (GameManager.Instance != null) {
            GameManager.Instance.OnCraftingStateStarted += Handle_OnCraftingStateStarted;
            Debug.Log("LISTENING!");
        }
    }

    private void OnDisable() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnCraftingStateStarted -= Handle_OnCraftingStateStarted;
        }
    }

    private void Handle_OnCraftingStateStarted(GameState state) {
        inventoryPanelUI.RefreshInventory();
        shopDisplayUI.RefreshShopSlots();
    }

}
