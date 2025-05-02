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
    private ResultManager resultManager;

    private int maxAmount;
    private int currentPrice;

    public void Setup(ResultManager resultManager, LootItemDataSO entry, AdventurerDataSO owner, int price, int quantity) {
        lootDropEntry = entry;
        adventurerRef = owner;
        this.resultManager = resultManager;

        itemIconUI.SetLootItem(entry);
        quantityPriceText.text = $"x {quantity}, price: {price}g";

        maxAmount = quantity;
        currentPrice = price;

        quantitySlider.Initialize(quantity);
        quantitySlider.OnValueChanged += OnQuantityChanged;
    }

    private void OnQuantityChanged(int newQuantity) {
        //resultPanel.NotifyItemPurchaseChanged(adventurerRef, lootDropEntry, newQuantity, currentPrice);
    }
}
