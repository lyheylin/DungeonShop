using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LootDropEntry {
    [SerializeField] private LootItemDataSO lootItem;
    [Range(0f, 1f)]
    [SerializeField] private float dropRate;

    public LootItemDataSO GetItem() => lootItem;
    public float GetDropRate() => dropRate;
}