using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DungeonManager : MonoBehaviour {
    public List<DungeonRunAssignment> currentAssignments;
    [SerializeField] private List<DungeonAreaSO> availableDungeons;
    [SerializeField] private List<AdventurerDataSO> availableAdventurers;

    public List<DungeonAreaSO> GetAvailableDungeons() => availableDungeons;
    public List<AdventurerDataSO> GetAvailableAdventurers () => availableAdventurers;


    public static DungeonManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartDungeonPhase(Dictionary<DungeonAreaSO, List<AdventurerDataSO>> assignments) {
        // Implement your dungeon simulation logic using the assignments here.
        Debug.Log("Dungeon simulation starting with assignments:");
        foreach (var kvp in assignments) {
            Debug.Log($"{kvp.Key.GetAreaName()}: {string.Join(", ", kvp.Value.Select(a => a.GetAdventurerName()))}");
        }

        // Transition to Dungeon phase or begin simulation...
    }


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