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
using Debug = UnityEngine.Debug;

public class ResultManager : MonoBehaviour {
    public static ResultManager Instance { get; private set; }

    private Dictionary<(AdventurerDataSO, LootItemDataSO), PurchaseData> currentPurchases = new();
    private List<AdventurerDataSO> activeAdventurers;
    private int availableGold;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void InitializeResults(List<AdventurerDataSO> adventurers, int startingGold) {
        activeAdventurers = adventurers;
        availableGold = startingGold;
        currentPurchases.Clear();

        foreach (var adventurer in adventurers) {
            foreach (var entry in adventurer.GetLootItems()) {
                var key = (adventurer, entry.LootItemDataSO);
                currentPurchases[key] = new PurchaseData {
                    owner = adventurer,
                    item = entry.LootItemDataSO,
                    pricePerUnit = entry.LootItemDataSO.GetBaseSellingPrice(),
                    maxQuantity = entry.Quantity,
                    selectedQuantity = entry.Quantity
                };
            }
        }
    }

    public IReadOnlyList<AdventurerDataSO> GetAdventurers() => activeAdventurers;

    public List<PurchaseData> GetLootForAdventurer(AdventurerDataSO adventurer) {
        return currentPurchases.Values
            .Where(p => p.owner == adventurer)
            .ToList();
    }

    public int GetTotalCost() {
        return currentPurchases.Values.Sum(p => p.selectedQuantity * p.pricePerUnit);
    }

    public int GetAvailableGold() => availableGold;

    public bool CanAfford() => GetTotalCost() <= availableGold;

    public void NotifyItemPurchaseChanged(AdventurerDataSO adventurer, LootItemDataSO lootItem, int quantity, int unitPrice) {
        Debug.Log($"Purchase quantity changed.");
        var key = (adventurer, lootItem);

        if (!currentPurchases.TryGetValue(key, out var data)) {
            Debug.Log($"PurchaseData not found for {adventurer.name} - {lootItem.name}");
            return;
        }

        data.selectedQuantity = quantity;
        Debug.Log($"Total gold needed is now {GetTotalCost()}g");
    }

    public void SetSelectedQuantity(AdventurerDataSO owner, LootItemDataSO item, int quantity) {
        var key = (owner, item);
        if (currentPurchases.ContainsKey(key)) {
            currentPurchases[key].selectedQuantity = quantity;
        }
    }

    public void ConfirmPurchases() {
        int totalCost = GetTotalCost();
        if (totalCost > availableGold) return;

        availableGold -= totalCost;

        foreach (var entry in currentPurchases.Values) {
            // Placeholder: affect adventurers later
            // InventorySystem.Instance.AddItem(entry.item, entry.selectedQuantity);
        }
    }

    public class PurchaseData {
        public AdventurerDataSO owner;
        public LootItemDataSO item;
        public int pricePerUnit;
        public int maxQuantity;
        public int selectedQuantity;
    }
}
