using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory Instance { get; private set; }
    private List<InventorySlot> items = new List<InventorySlot>();

    public IReadOnlyList<InventorySlot> GetItems() => items;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
    }

    public void AddItem(ItemDataSO itemData, int amount = 1) {
        var slot = items.Find(i => i.GetItemData() == itemData);

        if (slot != null) {
            slot.AddQuantity(amount);
        } else {
            var newSlot = new InventorySlot(itemData, amount);
            items.Add(newSlot);
        }

        Debug.Log($"Added {amount}x {itemData.GetName()} to inventory.");
    }

    public bool RemoveItem(ItemDataSO itemData, int amount = 1) {
        var slot = items.Find(i => i.GetItemData() == itemData);
        if (slot == null || slot.GetQuantity() < amount)
            return false;

        slot.RemoveQuantity(amount);
        if (slot.GetQuantity() == 0)
            items.Remove(slot);

        Debug.Log($"Removed {amount}x {itemData.GetName()} from inventory.");
        return true;
    }

    public bool HasItem(ItemDataSO itemData, int amount = 1) {
        var slot = items.Find(i => i.GetItemData() == itemData);
        return slot != null && slot.GetQuantity() >= amount;
    }

    public void ListItems() {
        Debug.Log($"Inventory List: ");
        foreach(InventorySlot item in items) {
            Debug.Log($"{item.GetQuantity()} x {item.GetItemData().GetName()}"); 
        }
    }
}