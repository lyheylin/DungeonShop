using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LootRarity {
    //PH
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "NewLootItem", menuName = "Game/Loot Item")]
public class LootItemDataSO : ScriptableObject {
    [SerializeField] private string lootName;
    [SerializeField] private string description;
    [SerializeField] private LootRarity rarity;
    [SerializeField] private Sprite icon;

    public string GetName() => lootName;
    public string GetDescription() => description;
    public LootRarity GetRarity() => rarity;
    public Sprite GetIcon() => icon;
}
