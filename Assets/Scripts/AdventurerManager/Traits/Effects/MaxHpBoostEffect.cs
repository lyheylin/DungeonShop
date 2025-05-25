using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/EffectBases/MaxHPBoost")]
public class MaxHpBoostEffect : TraitEffectBase {
    [SerializeField] private int _hpBoost;

    public MaxHpBoostEffect(int hpBoost) {
        _hpBoost = hpBoost;
    }

    public override void ApplyEffect(AdventurerRuntimeData adventurer) {
        adventurer.MaxHP += _hpBoost;
    }
}
