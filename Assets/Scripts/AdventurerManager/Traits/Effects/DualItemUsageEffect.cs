using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualItemUsageEffect : ITraitEffect {
    public void ApplyEffect(AdventurerRuntimeData adventurer) {
        adventurer.SetCanUseMultipleItems(true);
    }
}