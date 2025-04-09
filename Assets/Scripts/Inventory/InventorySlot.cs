using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot {
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private int quantity;


    public InventorySlot(ItemDataSO item, int quantity) {
        this.itemData = item;
        this.quantity = quantity;
    }
    public ItemDataSO GetItemData() => itemData;
    public int GetQuantity() => quantity;

    public void AddQuantity(int amount) => quantity += amount;
    public void RemoveQuantity(int amount) => quantity = Mathf.Max(0, quantity - amount);
}