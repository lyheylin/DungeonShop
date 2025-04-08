using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Game/Adventurer")]
public class AdventurerDataSO : ScriptableObject {
    public string adventurerName;
    public int baseHP;
    public int baseAttack;
    public int baseDefense;
    public Sprite portrait;

    // Future expansion: class type, equipment preferences, traits, etc.
}
