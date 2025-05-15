using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonArea", menuName = "Dungeon/DungeonArea")]
public class DungeonAreaSO : ScriptableObject {
    [SerializeField] private string areaName;
    [SerializeField] private List<MonsterDataSO> monsters;
    [TextArea][SerializeField] private string description;
    [SerializeField] private Sprite areaIcon;
    [SerializeField] private List<DungeonTrait> areaTraits;
    public List<DungeonTrait> GetAreaTraits() => areaTraits;
    public string GetAreaName() => areaName;
    public List<MonsterDataSO> GetMonsters() => monsters;
    public string GetDescription() => description;
    public Sprite GetAreaIcon() => areaIcon;
}