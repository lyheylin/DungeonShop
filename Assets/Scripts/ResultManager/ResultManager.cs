using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.UI.GridLayoutGroup;

public class ResultManager : MonoBehaviour {
    [SerializeField] private Transform resultListParent;
    [SerializeField] private AdventurerLootEntryUI advLootEntryPrefab;
    [SerializeField] private LootEntryUI lootEntryPrefab;
    [SerializeField] private TMP_Text totalGoldText;
    [SerializeField] private TMP_Text availableGoldText;
    [SerializeField] private Button confirmPurchaseButton;

    private Dictionary<(AdventurerDataSO, LootItemDataSO), PurchaseData> currentPurchases = new();
    private int availableGold;


    public static ResultManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetupResults(List<AdventurerDataSO> adventurers, int startingGold) {
        availableGold = startingGold;
        currentPurchases.Clear();
        ClearPreviousEntries();

        foreach (var adventurer in adventurers) {
            var ui = Instantiate(advLootEntryPrefab, resultListParent);
            ui.Setup(adventurer);

            var lootItems = adventurer.GetLootItems(); 
            foreach (var entry in lootItems) {
                CreateLootEntryUI(ui, adventurer, entry.LootItemDataSO, entry.LootItemDataSO.GetBaseSellingPrice(), entry.Quantity);
            }
        }

        UpdateGoldDisplay();
    }

    private void CreateLootEntryUI(AdventurerLootEntryUI containingUI, AdventurerDataSO adventurer, LootItemDataSO item, int price, int quantity) {
        var ui = Instantiate(lootEntryPrefab, containingUI.GetContainer());
        ui.Setup(this, item, adventurer, price, quantity);

        var key = (adventurer, item);
        currentPurchases[key] = new PurchaseData {
            owner = adventurer,
            item = item,
            pricePerUnit = price,
            maxQuantity = quantity,
            selectedQuantity = quantity
        };
    }

    public void NotifyItemPurchaseChanged(AdventurerDataSO adventurer, LootItemDataSO lootItem, int quantity, int unitPrice) {
        var key = (adventurer, lootItem);
        if (!currentPurchases.ContainsKey(key)) return;

        currentPurchases[key].selectedQuantity = quantity;
        UpdateGoldDisplay();
    }

    private void UpdateGoldDisplay() {
        int totalCost = currentPurchases.Values.Sum(p => p.selectedQuantity * p.pricePerUnit);
        totalGoldText.text = $"Total: {totalCost}g";
        availableGoldText.text = $"Gold: {availableGold}g";

        confirmPurchaseButton.interactable = totalCost <= availableGold;
    }

    private void ClearPreviousEntries() {
        foreach (Transform child in resultListParent)
            Destroy(child.gameObject);
    }

    public void ConfirmPurchases() {
        int totalCost = currentPurchases.Values.Sum(p => p.selectedQuantity * p.pricePerUnit);
        if (totalCost > availableGold) return;

        availableGold -= totalCost;

        foreach (var entry in currentPurchases.Values) {
            // You may want to log, animate, or influence adventurer behavior here
            //entry.owner.RecordLootSale(entry.item, entry.selectedQuantity, entry.pricePerUnit);
            //InventorySystem.Instance.AddItem(entry.item, entry.selectedQuantity);
        }

        // You could transition to the next phase here
    }

    private class PurchaseData {
        public AdventurerDataSO owner;
        public LootItemDataSO item;
        public int pricePerUnit;
        public int maxQuantity;
        public int selectedQuantity;
    }
}