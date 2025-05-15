using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerRuntimeData {
    public AdventurerDataSO AdventurerBase;
    public int CurrentHP;
    public int MaxHP;
    public int Attack;
    public int Defense;
    public int Alertness;
    public int LightLevel;
    public List<AdventurerTrait> ActiveTraits;
    public List<AdventurerInventoryItem> ActiveItems;

    public AdventurerRuntimeData(AdventurerDataSO baseData) {
        AdventurerBase = baseData;
        // Initialize stats based on base + equipment + skills
    }

    public void ApplyConsumable(ItemDataSO item) {
        // Use logic based on item effect
    }
}