using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonManager : MonoBehaviour {
    public List<DungeonRunAssignment> currentAssignments;

    public void AssignAdventurer(AdventurerDataSO adventurer, DungeonAreaSO area) {
        currentAssignments.Add(new DungeonRunAssignment {
            adventurer = adventurer,
            targetArea = area
        });
    }

    public void StartDungeonPhase() {
        foreach (var assignment in currentAssignments) {
            SimulateDungeonRun(assignment.adventurer, assignment.targetArea);
        }
    }

    private void SimulateDungeonRun(AdventurerDataSO adventurer, DungeonAreaSO area) {
        List<LootItemDataSO> totalLoot = new List<LootItemDataSO>();

        // For now: pick 3 random monsters and simulate drops
        List<MonsterDataSO> monsters = area.GetMonsters();
        for (int i = 0; i < 3; i++) {

            var monster = monsters[Random.Range(0, monsters.Count)];
            var drops = GetLootFromMonster(monster);
            totalLoot.AddRange(drops);
        }

        // Add loot to adventurer's inventory (if it exists)
        adventurer.AddLoot(totalLoot);
    }

    private List<LootItemDataSO> GetLootFromMonster(MonsterDataSO monster) {
        List<LootItemDataSO> drops = new List<LootItemDataSO>();
        foreach (var entry in monster.GetDropTable().lootEntries) {
            if (Random.value <= entry.GetDropRate()) {
                drops.Add(entry.GetItem());
            }
        }
        return drops;
    }
}