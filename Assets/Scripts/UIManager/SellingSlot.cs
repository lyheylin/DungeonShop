using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellingSlot : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_InputField quantityInput;
    [SerializeField] private TMP_InputField priceInput;

    private ItemDataSO itemData;
    private int quantity;

    public void Initialize(ItemDataSO item, int quantity) {
        itemData = item;

        iconImage.sprite = item.GetIcon();
        nameText.text = item.GetName();
        quantityInput.text = 0.ToString();
        priceInput.text = item.GetBasePrice().ToString();
        this.quantity = quantity;
    }

    public int GetCustomPrice() {
        if (int.TryParse(priceInput.text, out int price))
            return Mathf.Max(1, price);
        return itemData.GetBasePrice(); // Fallback
    }

    public int GetQuantity() {
        if (int.TryParse(quantityInput.text, out int quantity)) { 
            return Mathf.Max(0, Mathf.Min(this.quantity, quantity));
        }
        return 0; // Fallback
    }

    public void OnQuantityEdit() {
        if (int.TryParse(quantityInput.text, out int quantity)) {
            quantityInput.text = Mathf.Max(0, Mathf.Min(this.quantity, quantity)).ToString();
        } else
            quantityInput.text = string.Empty;
    }

    public ItemDataSO GetItemData() => itemData;
}