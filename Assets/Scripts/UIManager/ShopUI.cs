using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private Transform gridRoot;
    private void OnEnable() {
        RefreshShopSlots();
        if (GameManager.Instance != null) {
        }
    }
    private void OnDisable() {
        if (GameManager.Instance != null) {
        }
    }

    //PH
    private const int MaxShopSlots = 6; // Expandable later
    private List<ShopSlot> sellingSlots = new List<ShopSlot>();

    public void RefreshShopSlots() {
        foreach (Transform child in gridRoot)
            Destroy(child.gameObject);

        int slotsFilled = 0;
        foreach(var item in ShopManager.Instance.GetShopItems()) {
            GameObject slotGO = Instantiate(shopSlotPrefab, gridRoot);
            ShopSlot slot = slotGO.GetComponent<ShopSlot>();
            slot.Initialize(item.GetItemData(), item.GetQuantity(), item.GetPrice());

            sellingSlots.Add(slot);
            slotsFilled++;
        }
    }
}
