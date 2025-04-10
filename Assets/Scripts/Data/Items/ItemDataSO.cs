using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Material,
    Equipment,
    Consumable,
    Loot,
    KeyItem
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemDataSO : ScriptableObject {
    //PH
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private ItemType itemType;
    [SerializeField] private Sprite icon;

    [Header("Shop Properties")]
    [SerializeField] private bool isSellable;
    [SerializeField] private int basePrice;

    public string GetName() => itemName;
    public string GetDescription() => description;
    public ItemType GetItemType() => itemType;
    public Sprite GetIcon() => icon;

    public bool IsSellable() => isSellable;
    public int GetBasePrice() => basePrice;
}