using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySaveData {
    public List<InventoryItemEntry> items = new List<InventoryItemEntry>();
}

[Serializable]
public class InventoryItemEntry {
    public string itemName;
    public int quantity;
}