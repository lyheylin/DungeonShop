using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpBoostEffect : ITraitEffect {
    private readonly int _hpBoost;

    public MaxHpBoostEffect(int hpBoost) {
        _hpBoost = hpBoost;
    }

    public void ApplyEffect(AdventurerRuntimeData adventurer) {
        adventurer.MaxHP += _hpBoost;
    }
}
