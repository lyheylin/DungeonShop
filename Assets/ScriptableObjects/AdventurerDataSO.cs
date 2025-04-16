using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class AdventurerInventoryItem {
    public ItemDataSO ItemData;
    public int Quantity;

    public AdventurerInventoryItem(ItemDataSO itemData, int quantity = 1) {
        ItemData = itemData;
        Quantity = quantity;
    }
}

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Game/Adventurer")]
public class AdventurerDataSO : ScriptableObject {
    [SerializeField] private string adventurerName;
    [SerializeField] private int baseHP;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    public Sprite portrait;
    [SerializeField] private List<AdventurerInventoryItem> inventory = new List<AdventurerInventoryItem>();
    [SerializeField] private ItemDataSO equippedItem;
    public string GetAdventurerName() => adventurerName;
    public int GetBaseHP() => baseHP;
    public int GetBaseAttack() => baseAttack;
    public int GetBaseDefense() => baseDefense;

    public ItemDataSO GetEquippedItem() => equippedItem;
    public List<AdventurerInventoryItem> GetInventory() => inventory;

    public void AddToInventory(ItemDataSO item, int quantity = 1) {
        var existingItem = inventory.Find(i => i.ItemData == item);
        if (existingItem != null) 
            existingItem.Quantity += quantity;
        else 
            inventory.Add(new AdventurerInventoryItem(item, quantity));
    }

    public void ClearInventory() {
        inventory.Clear();
    }

    public bool WillBuyItem(ShopItem item) {
        int acceptablePrice = item.GetItemData().GetBasePrice() * 2;
        return item.GetPrice() <= acceptablePrice;
    }

    public void EquipItem(ItemDataSO item) {
        equippedItem = item;
    }

    public List<AdventurerInventoryItem> GetConsumables() {
        return inventory.Where(i => i.ItemData.GetItemType() == ItemType.Consumable).ToList();
    }
    public List<AdventurerInventoryItem> GetEquipment() {
        return inventory.Where(i => i.ItemData.GetItemType() == ItemType.Equipment).ToList();
    }

    public void ChooseEquipmentAndEquip() {
        var equipmentList = GetEquipment();
        if (equipmentList.Count > 0) {
            EquipItem(equipmentList[0].ItemData);
            LogManager.Instance.Log($"{GetAdventurerName()} equipped {equipmentList[0].ItemData.GetName()}.");
        }
    }
    // Future expansion: class type, equipment preferences, traits, etc.
}
