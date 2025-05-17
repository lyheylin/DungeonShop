using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TraitEffectType {
    MaxHPBoost,
    StrengthMultiplier,
    //
}
public static class TraitEffectFactory {
    public static ITraitEffect CreateEffect(TraitEffectType type, float value) {
        return type switch {
            TraitEffectType.MaxHPBoost => new MaxHpBoostEffect(Mathf.RoundToInt(value)),
            _ => null
        };
    }
}