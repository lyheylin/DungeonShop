using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TraitEffectType {
    MaxHPBoost,
    StrengthMultiplier,
    ToughExplorer,
    Resourceful
    //
}
public static class TraitEffectFactory {
    public static ITraitEffect CreateEffect(TraitEffectType type, float value) {
        return type switch {
            TraitEffectType.MaxHPBoost => new MaxHpBoostEffect(Mathf.RoundToInt(value)),
            TraitEffectType.ToughExplorer => new CompositeTraitEffect(new MaxHpBoostEffect(20), new AttackBoostEffect(2)),
            TraitEffectType.Resourceful => new DualItemUsageEffect(),
            _ => null
        };
    }
}