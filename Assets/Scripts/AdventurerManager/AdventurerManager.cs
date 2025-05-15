using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerManager : MonoBehaviour {
    public static AdventurerManager Instance { get; private set; }
    [SerializeField] private List<AdventurerDataSO> adventurers;
    private List<Adventurer> activeAdventurers = new List<Adventurer>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAdventurers();
    }

    public void InitializeAdventurers() {
        activeAdventurers.Clear();

        foreach (var adventurer in adventurers) {
            Adventurer newAdventurer = new Adventurer(adventurer);
            activeAdventurers.Add(newAdventurer);
        }

        Debug.Log($"Initialized {activeAdventurers.Count} adventurers.");
    }

    public List<Adventurer> GetActiveAdventurers() {
        return activeAdventurers;
    }




    public List<AdventurerSaveData> GetSaveData() {
        var saveDataList = new List<AdventurerSaveData>();
        foreach (var adv in adventurers) {
            var data = new AdventurerSaveData {
                adventurerName = adv.GetAdventurerName(),
                equippedItemName = adv.GetEquippedItem() != null ? adv.GetEquippedItem().GetName() : ""
            };

            // Inventory items with quantities
            foreach (var item in adv.GetInventory()) {
                var existingEntry = data.items.Find(entry => entry.itemName == item.ItemData.GetName());
                int quantity = item.Quantity;
                if (existingEntry != null)
                    existingEntry.quantity += quantity;
                else
                    data.items.Add(new ItemEntry { itemName = item.ItemData.GetName(), quantity = quantity});
            }

            saveDataList.Add(data);
        }
        return saveDataList;
    }

    public void LoadFromSaveData(List<AdventurerSaveData> dataList, ItemDatabase itemDatabase) {
        foreach (var data in dataList) {
            var adventurer = adventurers.Find(a => a.GetAdventurerName() == data.adventurerName);
            if (adventurer == null) continue;

            adventurer.ClearInventory();

            foreach (var itemEntry in data.items) {
                var itemSO = itemDatabase.GetItemByName(itemEntry.itemName);
                if (itemSO != null) {
                    adventurer.AddToInventory(itemSO, itemEntry.quantity);
                }
            }

            adventurer.EquipItem(string.IsNullOrEmpty(data.equippedItemName)
                ? null
                : itemDatabase.GetItemByName(data.equippedItemName));
        }
    }

}
