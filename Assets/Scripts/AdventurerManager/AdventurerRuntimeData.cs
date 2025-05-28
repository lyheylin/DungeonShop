using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class AdventurerRuntimeData {
    public AdventurerDataSO AdventurerBase;
    public int CurrentHP;
    public int MaxHP;
    public int Attack;
    public int Defense;
    public int Alertness;
    public int LightLevel;
    public List<AdventurerTraitSO> ActiveTraits;
    public List<AdventurerInventoryItem> ActiveItems;

    public AdventurerRuntimeData(AdventurerDataSO baseData) {
        AdventurerBase = baseData;
        // Initialize stats based on base + equipment + skills
        //TODO extract, revise and isolate formulas

        MaxHP = Mathf.RoundToInt(baseData.GetConstitution() * 1.5f);
        CurrentHP = MaxHP;
        Attack = baseData.GetStrength();
        Defense = Mathf.RoundToInt(baseData.GetConstitution() * 0.2f + baseData.GetAgility() * 0.8f);
        Alertness = 0;
        LightLevel = 0;

        ActiveTraits = baseData.GetTraits();

        ApplyTraits();
    }

    public void ApplyConsumable(ItemDataSO item) {
        // Use logic based on item effect
    }

    private void ApplyTraits() {
        foreach (var trait in ActiveTraits) {
            trait.ApplyAllEffects(this);
        }
    }




    // Specific implementations
    private bool canUseMultipleItems = false;
    public bool CanUseMultipleItems() => canUseMultipleItems;
    internal void SetCanUseMultipleItems(bool value) {
        canUseMultipleItems = value;
    }
}