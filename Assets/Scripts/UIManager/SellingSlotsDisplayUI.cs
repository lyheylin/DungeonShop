using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SellingSlotsDisplayUI : MonoBehaviour {
    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private Transform gridRoot;

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

            GameObject slotGO = Instantiate(shopSlotPrefab, gridRoot);
            SellingSlot slot = slotGO.GetComponent<SellingSlot>();
            slot.Initialize(kvp.GetItemData(), kvp.GetQuantity());

            sellingSlots.Add(slot);
            slotsFilled++;
        }
    }

    public void MoveItemsToShop() {
        foreach (SellingSlot slot in sellingSlots) {
            if (slot.GetQuantity() > 0) { 
                Inventory.Instance.RemoveItem(slot.GetItemData(), slot.GetQuantity());
                ShopManager.Instance.AddItemToShop(slot.GetItemData(), slot.GetQuantity(), slot.GetCustomPrice());
                Debug.Log($"{slot.GetItemData().GetName()} x {slot.GetQuantity()} moved to shop at {slot.GetCustomPrice()} gold.");
                Destroy(slot);
            }
        }
        sellingSlots = new List<SellingSlot>();
    }
}