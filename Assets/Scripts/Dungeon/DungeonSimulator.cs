using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSimulator : MonoBehaviour {
    public void RunDungeon() {
        Debug.Log($"Running dungeon!");
        foreach (var adventurer in AdventurerManager.Instance.GetActiveAdventurers()) {
            int difficulty = Random.Range(10, 50); // Placeholder floor difficulty

            int power = adventurer.currentAttack + adventurer.currentDefense;

            if (power > difficulty) {
                adventurer.survivedLastRun = true;
                Debug.Log($"{adventurer.GetName()} survived and brought back loot!");
            } else {
                adventurer.survivedLastRun = false;
                Debug.Log($"{adventurer.GetName()} was defeated in the dungeon...");
            }
        }
    }
}
