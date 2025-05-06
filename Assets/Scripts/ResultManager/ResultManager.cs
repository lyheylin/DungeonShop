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

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void InitializeResults(List<AdventurerDataSO> adventurers) {
        activeAdventurers = adventurers;
        currentPurchases.Clear();

        foreach (var adventurer in adventurers) {
            foreach (var entry in adventurer.GetLootItems()) {
                var key = (adventurer, entry.LootItemDataSO);
                currentPurchases[key] = new PurchaseData {
                    owner = adventurer,
                    item = entry.LootItemDataSO,
                    pricePerUnit = entry.LootItemDataSO.GetBaseSellingPrice(),
                    maxQuantity = entry.Quantity,
                    selectedQuantity = 0
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

    public int GetAvailableGold() => ShopManager.Instance.GetGold();

    public bool CanAfford() => GetTotalCost() <= GetAvailableGold();

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
        if (totalCost > GetAvailableGold()) return;

        ShopManager.Instance.RemoveGold(totalCost);

        foreach (var entry in currentPurchases.Values) {
            if (entry.selectedQuantity <= 0) continue;

            ItemDataSO itemToAdd = entry.item;

            // If it's loot, check if it has a converted item version
            if (entry.item is LootItemDataSO lootItem) {
                ItemDataSO converted = lootItem.GetConvertedItem(); // Define this on LootItemDataSO
                itemToAdd = converted != null ? converted : lootItem;
            }
            entry.owner.RemoveLoot(entry.item, entry.selectedQuantity);
            Inventory.Instance.AddItem(itemToAdd, entry.selectedQuantity);

            // Optional: Record the sale if you want adventurers to remember they sold it
            // entry.owner.RecordLootSale(entry.item, entry.selectedQuantity, entry.pricePerUnit);
        }

        currentPurchases = new();
        InitializeResults(activeAdventurers);
    }

    public class PurchaseData {
        public AdventurerDataSO owner;
        public LootItemDataSO item;
        public int pricePerUnit;
        public int maxQuantity;
        public int selectedQuantity;
    }
}
