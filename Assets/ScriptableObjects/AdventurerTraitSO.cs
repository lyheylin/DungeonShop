using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Game/AdventurerTrait")]
public class AdventurerTraitSO : ScriptableObject {
    [SerializeField] private string traitName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    private List<ITraitEffect> effects = new();

    public string GetTraitName() => traitName;
    public string GetDescription() => description;
    public Sprite GetIcon() => icon;

    public void AddEffect(ITraitEffect effect) => effects.Add(effect);

    public void ApplyAllEffects(AdventurerRuntimeData data) {
        foreach (var effect in effects) {
            effect.ApplyEffect(data);
        }
    }
}