using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopSlot : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;


    private ItemDataSO itemData;
    private int quantity;
    private int price;
    public ItemDataSO GetItemData() => itemData;
    public int GetQuantity() => quantity;
    public int GetPrice() => price;

    public void Initialize(ItemDataSO item, int quantity, int price) {
        itemData = item;

        iconImage.sprite = item.GetIcon();
        nameText.text = $"{item.GetName()} x {quantity.ToString()}";
        priceText.text = $"{price.ToString()} G";
        this.quantity = quantity;
        this.price = price;
    }

}