using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Game/Adventurer")]
public class AdventurerDataSO : ScriptableObject {
    [SerializeField] private string adventurerName;
    [SerializeField] private int baseHP;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    public Sprite portrait;


    public string GetAdventurerName() => adventurerName;
    public int GetBaseHP() => baseHP;
    public int GetBaseAttack() => baseAttack;
    public int GetBaseDefense()=> baseDefense;

    // Future expansion: class type, equipment preferences, traits, etc.
}
