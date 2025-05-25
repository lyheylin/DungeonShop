using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TraitEffectBase : ScriptableObject, ITraitEffect {
    public abstract void ApplyEffect(AdventurerRuntimeData adventurerData);
}