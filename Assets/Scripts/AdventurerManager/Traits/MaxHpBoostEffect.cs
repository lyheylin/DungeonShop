using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpBoostEffect : ITraitEffect {
    private readonly float _hpBoost;

    public MaxHpBoostEffect(float hpBoost) {
        _hpBoost = hpBoost;
    }

    public void ApplyEffect(AdventurerRuntimeData adventurer) {
        adventurer.MaxHP = (int)(adventurer.MaxHP*_hpBoost);
    }
}
