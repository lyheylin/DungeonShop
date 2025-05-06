using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LootRarity {
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Game/Loot Item")]
public class LootItemDataSO : ItemDataSO {

    [SerializeField] private ItemDataSO convertedItem;
    [Header("Loot Properties")]
    [SerializeField] private LootRarity rarity;

    [TextArea]
    [SerializeField] private string adventurerDescription;

    private void OnEnable() {
        itemType = ItemType.Loot;
    }

    public LootRarity GetRarity() => rarity;

    // Override base description with player description
    public override string GetDescription() => description;

    public string GetAdventurerDescription() => adventurerDescription;

    public int GetBaseSellingPrice() => basePrice;

    public ItemDataSO GetConvertedItem() => convertedItem;
}