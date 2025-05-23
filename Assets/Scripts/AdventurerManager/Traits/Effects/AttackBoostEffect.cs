using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoostEffect : ITraitEffect {
    private readonly int _amount;

    public AttackBoostEffect(int amount) {
        _amount = amount;
    }

    public void ApplyEffect(AdventurerRuntimeData adventurer) {
        adventurer.Attack += _amount;
    }
}