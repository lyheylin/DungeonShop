using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootEntryUI : MonoBehaviour {
    [Header("UI References")]
    [SerializeField] private ItemIconWithTooltipUI itemIconUI;
    [SerializeField] private TMP_Text quantityPriceText;
    [SerializeField] private QuantitySliderUI quantitySlider;

    private LootItemDataSO lootDropEntry;
    private AdventurerDataSO adventurerRef;

    private int maxAmount;
    private int currentPrice;

    public void Setup(ResultPanelUI panelUI, LootItemDataSO entry, AdventurerDataSO owner, int price, int quantity) {
        lootDropEntry = entry;
        adventurerRef = owner;

        itemIconUI.SetLootItem(entry);
        quantityPriceText.text = $"x {quantity}, price: {price}g";

        maxAmount = quantity;
        currentPrice = price;

        quantitySlider.Initialize(quantity);
        quantitySlider.OnValueChanged += newQuantity => {
            panelUI.NotifyQuantityChanged(owner, entry, newQuantity);
            ResultManager.Instance.NotifyItemPurchaseChanged(adventurerRef, lootDropEntry, newQuantity, currentPrice);
        };
    }
}


