using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/EffectBases/AttackBoost")]
public class AttackBoostEffect : TraitEffectBase {
    [SerializeField] private float _amount;

    public AttackBoostEffect(float amount) {
        _amount = amount;
    }

    public override void ApplyEffect(AdventurerRuntimeData adventurer) {
        adventurer.Attack = Mathf.RoundToInt(adventurer.Attack*_amount);
    }
}