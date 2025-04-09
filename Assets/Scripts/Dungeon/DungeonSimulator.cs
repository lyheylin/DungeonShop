using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSimulator : MonoBehaviour {

    //PH
    [SerializeField] private List<LootItemDataSO> possibleLoot;
    [SerializeField] private ItemDatabase itemDatabase;

    public void RunDungeon() {
        Debug.Log($"Running dungeon!");

        Inventory inventory = FindObjectOfType<Inventory>();
        foreach (var adventurer in AdventurerManager.Instance.GetActiveAdventurers()) {
            int difficulty = Random.Range(10, 50); //PH floor difficulty

            int power = adventurer.GetCurrentAttack() + adventurer.GetCurrentDefense();

            if (power > difficulty) {
                adventurer.SetSurvivedLastRun(true);

                //PH Result loot
                var loot = GenerateRandomLoot();
                adventurer.ReceiveLoot(loot);
                Debug.Log($"{adventurer.GetName()} survived and brought back loot!");


                //PH
                ItemDataSO itemForInventory = itemDatabase.GetItemByName(loot.GetName());
                if (itemForInventory != null) {
                    inventory.AddItem(itemForInventory);
                }

            } else {
                adventurer.SetSurvivedLastRun(false);
                Debug.Log($"{adventurer.GetName()} was defeated in the dungeon...");
            }
        }

        inventory.ListItems();
    }
    private LootItem GenerateRandomLoot() {
        if (possibleLoot.Count == 0) return null;

        int index = Random.Range(0, possibleLoot.Count);
        return new LootItem(possibleLoot[index]);
    }
}
