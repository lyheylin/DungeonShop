using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private GameObject sellingSlotPrefab;
    [SerializeField] private Transform gridRoot;
    private void OnEnable() {
        if (GameManager.Instance != null) {
        }
    }
    private void OnDisable() {
        if (GameManager.Instance != null) {
        }
    }

    //PH
    private const int MaxShopSlots = 6; // Expandable later
    private List<SellingSlot> sellingSlots = new List<SellingSlot>();

    public void RefreshShopSlots() {
        foreach (Transform child in gridRoot)
            Destroy(child.gameObject);

        int slotsFilled = 0;
        foreach (var kvp in Inventory.Instance.GetItems()) {
            if (!kvp.GetItemData().IsSellable()) continue;
            if (slotsFilled >= MaxShopSlots) break;

            GameObject slotGO = Instantiate(sellingSlotPrefab, gridRoot);
            SellingSlot slot = slotGO.GetComponent<SellingSlot>();
            slot.Initialize(kvp.GetItemData(), kvp.GetQuantity());

            sellingSlots.Add(slot);
            slotsFilled++;
        }
    }
}
