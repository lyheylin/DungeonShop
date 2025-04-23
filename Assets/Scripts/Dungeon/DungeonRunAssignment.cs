using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DungeonRunAssignment {
    public AdventurerDataSO adventurer;
    public DungeonAreaSO targetArea;

    public DungeonRunAssignment(AdventurerDataSO adventurer, DungeonAreaSO targetArea) {
        this.adventurer = adventurer;
        this.targetArea = targetArea;
    }
}