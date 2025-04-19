using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData{
    // future: expand as needed
    public InventorySaveData inventory;
    public TimeSaveData time;
    public List<AdventurerSaveData> adventurers;
    public ShopSaveData shop;
}


[Serializable]
public class ItemEntry {
    public string itemName;
    public int quantity;
}


//Inventory
[Serializable]
public class InventorySaveData {
    public List<ItemEntry> items = new List<ItemEntry>();
}

//Time: day, phase
[Serializable]
public class TimeSaveData {
    public int currentDay;
    public GameState currentGameState;
}

//Adventurer data
[Serializable]
public class AdventurerSaveData {
    public string adventurerName;
    public List<ItemEntry> items = new List<ItemEntry>();
    public string equippedItemName;
}

//Shop
[Serializable]
public class ShopSaveData {
    public List<ShopItemSaveData> shopItems;
}

[Serializable]
public class ShopItemSaveData {
    public string itemName;
    public int quantity;
    public int price;
}