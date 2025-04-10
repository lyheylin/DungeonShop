using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopSlot {
    public ItemDataSO item;
    public int quantity;
    public int price;

    public ShopSlot(ItemDataSO item, int quantity, int price) {
        this.item = item;
        this.quantity = quantity;
        this.price = price;
    }
}