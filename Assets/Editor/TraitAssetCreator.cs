
using UnityEngine;

public class TraitAssetCreator : MonoBehaviour
{
    [ContextMenu("Create Example Traits")]
    public void CreateExampleTraits()
    {
        CreateToughExplorerTrait();
        CreateResourcefulTrait();
    }

    private void CreateToughExplorerTrait()
    {
        AdventurerTraitSO trait = ScriptableObject.CreateInstance<AdventurerTraitSO>();
        trait.name = "Tough Explorer";
        trait.description = "A seasoned wanderer, hardened by countless dungeon treks.";
        trait.effectType = TraitEffectType.ToughExplorer;
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.CreateAsset(trait, "Assets/Data/Traits/ToughExplorer.asset");
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    private void CreateResourcefulTrait()
    {
        AdventurerTraitSO trait = ScriptableObject.CreateInstance<AdventurerTraitSO>();
        trait.name = "Resourceful";
        trait.description = "Always has a trick or two up their sleeve. Can use more than one item per turn.";
        trait.effectType = TraitEffectType.Resourceful;
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.CreateAsset(trait, "Assets/Data/Traits/Resourceful.asset");
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }
}
