using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem {
    private LootItemDataSO loot;

    public LootItem(LootItemDataSO lootItem) {
        loot = lootItem;
    }

    public string GetName() => loot.GetName();
    public LootRarity GetRarity() => loot.GetRarity();
    public LootItemDataSO GetData() => loot;
}
