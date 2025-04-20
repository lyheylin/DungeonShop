using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterLootTable", menuName = "Dungeon/Loot Table")]
public class MonsterLootTableSO : ScriptableObject {
    public List<LootDropEntry> lootEntries;
}