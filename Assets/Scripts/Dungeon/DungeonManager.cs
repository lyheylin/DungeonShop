using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DungeonManager : MonoBehaviour {
    [SerializeField] private List<DungeonAreaSO> availableDungeons;
    [SerializeField] private List<AdventurerDataSO> availableAdventurers;

    private List<DungeonRunAssignment> currentAssignments;
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
        currentAssignments = new List<DungeonRunAssignment> ();
        // Implement your dungeon simulation logic using the assignments here.
        Debug.Log("Dungeon simulation starting with assignments:");
        foreach (var kvp in assignments) {
            Debug.Log($"{kvp.Key.GetAreaName()}: {string.Join(", ", kvp.Value.Select(a => a.GetAdventurerName()))}");
            foreach (var adv in kvp.Value) {
                currentAssignments.Add(new DungeonRunAssignment(adv, kvp.Key));//Available for adding more variables into the assignment.
            }
        }

        foreach (var assignment in currentAssignments) {
            SimulateDungeonRun(assignment.adventurer, assignment.targetArea);
        }
        // Transition to Dungeon phase or begin simulation...
    }

    public void StartDungeonPhase() {
        foreach (var assignment in currentAssignments) {
            SimulateDungeonRun(assignment.adventurer, assignment.targetArea);
        }
    }

    private void SimulateDungeonRun(AdventurerDataSO adventurer, DungeonAreaSO area) {
        LogManager.Instance.Log($"{adventurer.GetAdventurerName()} exploring the {area.GetAreaName()} area.");
        List<LootItemDataSO> totalLoot = new List<LootItemDataSO>();

        // For now: pick 3 random monsters and simulate drops
        List<MonsterDataSO> monsters = area.GetMonsters();
        for (int i = 0; i < 3; i++) {
            var monster = monsters[Random.Range(0, monsters.Count)];
            var drops = GetLootFromMonster(monster);
            LogManager.Instance.Log($"...and fought a {monster.GetMonsterName()}.");
            foreach(var drop in drops) {
                LogManager.Instance.Log($"Received {drop.GetName()}.");
            }
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