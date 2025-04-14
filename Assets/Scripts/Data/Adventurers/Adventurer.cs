using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer {
    [SerializeField] private AdventurerDataSO adventurer;


    //PH
    private int currentHP;
    private int currentAttack;
    private int currentDefense;
    private bool survivedLastRun = true;

    private List<LootItem> collectedLoot = new List<LootItem>();

    public string GetName() => adventurer.GetAdventurerName();
    public int GetCurrentAttack() => currentAttack;
    public int GetCurrentDefense() => currentDefense;
    public bool DidSurvive() => survivedLastRun;
    public List<LootItem> GetCollectedLoot() => collectedLoot;
    public AdventurerDataSO GetAdventurerDataSO() => adventurer;

    public Adventurer(AdventurerDataSO adventurer) {
        this.adventurer = adventurer;
        currentHP = adventurer.GetBaseHP();
        currentAttack = adventurer.GetBaseAttack();
        currentDefense = adventurer.GetBaseDefense();
    }
    public void SetSurvivedLastRun(bool survived) {
        survivedLastRun = survived;
        if (!survived) collectedLoot.Clear(); // Optional: lose loot on failure //PH
    }

    public void ReceiveLoot(LootItem loot) {
        if (loot != null)
            collectedLoot.Add(loot);
    }
}
