using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Material,
    Equipment,
    Consumable,
    KeyItem
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject {
    public string itemName;
    public string description;
    public ItemType itemType;
    public Sprite icon;

    public int baseValue;

    // Crafting & stat bonuses (placeholder)
    public int power;
    public int durability;
    public int rarity;
}