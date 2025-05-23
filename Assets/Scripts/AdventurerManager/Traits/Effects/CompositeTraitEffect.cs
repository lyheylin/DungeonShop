using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeTraitEffect : ITraitEffect {
    private readonly List<ITraitEffect> _effects;

    public CompositeTraitEffect(params ITraitEffect[] effects) {
        _effects = new List<ITraitEffect>(effects);
    }

    public void ApplyEffect(AdventurerRuntimeData adventurer) {
        foreach (var effect in _effects)
            effect.ApplyEffect(adventurer);
    }
}