using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisplayUI : MonoBehaviour {
    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private Transform gridRoot;

    //PH
    private const int MaxShopSlots = 6; // Expandable later

    public void RefreshShopSlots() {
        foreach (Transform child in gridRoot)
            Destroy(child.gameObject);

        int slotsFilled = 0;
        foreach (var kvp in Inventory.Instance.GetItems()) {
            if (!kvp.GetItemData().IsSellable()) continue;
            if (slotsFilled >= MaxShopSlots) break;

            GameObject slotGO = Instantiate(shopSlotPrefab, gridRoot);
            ShopSlotUI slot = slotGO.GetComponent<ShopSlotUI>();
            slot.Initialize(kvp.GetItemData(), kvp.GetQuantity());

            slotsFilled++;
        }
    }
}