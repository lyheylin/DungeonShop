using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private TMP_InputField priceInput;

    private ItemDataSO itemData;

    public void Initialize(ItemDataSO item, int quantity) {
        itemData = item;

        iconImage.sprite = item.GetIcon();
        nameText.text = item.GetName();
        quantityText.text = $"x{quantity}";
        priceInput.text = item.GetBasePrice().ToString(); 
    }

    public int GetCustomPrice() {
        if (int.TryParse(priceInput.text, out int price))
            return Mathf.Max(1, price);
        return itemData.GetBasePrice(); // Fallback
    }

    public ItemDataSO GetItemData() => itemData;
}