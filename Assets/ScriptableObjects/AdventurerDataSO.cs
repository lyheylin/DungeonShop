using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.ParticleSystem;


[System.Serializable]
public class AdventurerInventoryItem {
    public ItemDataSO ItemData;
    public int Quantity;

    public AdventurerInventoryItem(ItemDataSO itemData, int quantity = 1) {
        ItemData = itemData;
        Quantity = quantity;
    }
}

public class AdventurerLootItem {
    public LootItemDataSO LootItemDataSO;
    public int Quantity;

    public AdventurerLootItem(LootItemDataSO lootItemDataSO, int quantity) {
        LootItemDataSO = lootItemDataSO;
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

    [SerializeField] private int strength;
    [SerializeField] private int agility;
    [SerializeField] private int constitution;
    [SerializeField] private int spirituality;
    [SerializeField] private int insight;

    [SerializeField] private List<AdventurerTraitSO> traits;
    private AdventurerRuntimeData adventurerRuntimeData;
    public int GetStrength() => strength;
    public int GetAgility() => agility;
    public int GetConstitution() => constitution;
    public int GetSpirituality() => spirituality;
    public int GetInsight() => insight;
    public List<AdventurerTraitSO> GetTraits() => traits;
    public AdventurerRuntimeData GetAdventurerRuntimeData() => adventurerRuntimeData;
    
    [SerializeField] private List<AdventurerInventoryItem> inventory = new List<AdventurerInventoryItem>();
    [SerializeField] private ItemDataSO equippedItem;
    private List<AdventurerLootItem> lootItems = new();
    public string GetAdventurerName() => adventurerName;
    public int GetBaseHP() => baseHP;
    public int GetBaseAttack() => baseAttack;
    public int GetBaseDefense() => baseDefense;

    public ItemDataSO GetEquippedItem() => equippedItem;
    public List<AdventurerInventoryItem> GetInventory() => inventory;

    public void InitiateAdventurer() {
        //TODO
    }

    public void ApplyTraits() {
        foreach (AdventurerTraitSO trait in GetTraits()) {
            ITraitEffect effect = trait.CreateEffectInstance();
            effect?.ApplyEffect(adventurerRuntimeData);
        }
    }

    public void AddToInventory(ItemDataSO item, int quantity = 1) {
        var existingItem = inventory.Find(i => i.ItemData == item);
        if (existingItem != null) 
            existingItem.Quantity += quantity;
        else 
            inventory.Add(new AdventurerInventoryItem(item, quantity));
    }


    public void AddLoot(List<LootItemDataSO> loots) {
        foreach(var loot in loots) {
            AddLoot(loot);
        }
    }
    public void AddLoot(LootItemDataSO loot) {
        var existingItem = lootItems.Find(i => i.LootItemDataSO == loot);
        if (existingItem != null)
            existingItem.Quantity += 1;
        else
            lootItems.Add(new AdventurerLootItem(loot, 1));
    }

    public void RemoveLoot(LootItemDataSO loot, int quantity) {
        var existingItem = lootItems.Find(i => i.LootItemDataSO == loot);
        if (existingItem != null) {
            existingItem.Quantity -= 1;
            if(existingItem.Quantity == 0)lootItems.Remove(existingItem);
        } else
            lootItems.Add(new AdventurerLootItem(loot, 1));
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

    public List<AdventurerLootItem> GetLootItems() {
        return lootItems;
    }
    // Future expansion: class type, equipment preferences, traits, etc.
}
