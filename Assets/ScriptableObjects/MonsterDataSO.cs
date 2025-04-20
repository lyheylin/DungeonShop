using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMonster", menuName = "Dungeon/Monster")]
public class MonsterDataSO : ScriptableObject {
    [SerializeField] private string monsterName;
    [SerializeField] private List<LootDropEntry> dropTable;
    [SerializeField] private List<MonsterTrait> traits;
    [SerializeField] private int baseHP;
    [SerializeField] private int baseATK;
    [SerializeField] private Sprite monsterSprite;

    public string GetMonsterName() => monsterName;
    public List<LootDropEntry> GetDropTable() => dropTable;
    public List<MonsterTrait> GetTraits() => traits;
    public int GetBaseHP() => baseHP;
    public int GetBaseATK() => baseATK;
    public Sprite GetMonsterSprite() => monsterSprite;
}