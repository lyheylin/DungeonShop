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
    [SerializeField] protected string itemName;
    [SerializeField] protected string description; // Player-facing description
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected Sprite icon;

    [Header("Shop Properties")]
    [SerializeField] protected bool isSellable;
    [SerializeField] protected int basePrice;

    public virtual string GetName() => itemName;
    public virtual string GetDescription() => description;
    public virtual Sprite GetIcon() => icon;
    public virtual ItemType GetItemType() => itemType;
    public virtual bool IsSellable() => isSellable;
    public virtual int GetBasePrice() => basePrice;
}