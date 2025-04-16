using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopItem
{

    private ItemDataSO itemData;
    private int quantity;
    private int price;
    public ItemDataSO GetItemData() => itemData;
    public int GetQuantity() => quantity;
    public int GetPrice() => price;

    public ShopItem(ItemDataSO itemData, int quantity, int price) {
        this.itemData = itemData;
        this.quantity = quantity;
        this.price = price;
    }

    public void RemoveItem(int quantity) {
        if (quantity > this.quantity) {
            Debug.LogWarning("Attempting to remove more items than possible.");
            return;
        }
        this.quantity -= quantity;
    }
}
