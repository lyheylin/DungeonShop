using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer {
    [SerializeField] private AdventurerDataSO adventurer;


    //placeholders
    private int currentHP;
    public int currentAttack;
    public int currentDefense;
    public bool survivedLastRun = true;

    public Adventurer(AdventurerDataSO adventurer) {
        this.adventurer = adventurer;
        this.currentHP = adventurer.baseHP;
        this.currentAttack = adventurer.baseAttack;
        this.currentDefense = adventurer.baseDefense;
    }

    public string GetName() {
        return adventurer.name;
    }
}
