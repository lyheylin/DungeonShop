using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    Weapon,
    Armor,
    Accessory
}

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Items/Equipment")]
public class EquipmentDataSO : ItemDataSO {
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private int bonusHP;
    [SerializeField] private int bonusAttack;
    [SerializeField] private int bonusDefense;

    [SerializeField] private List<AdventurerTraitSO> grantedTraits;

    public EquipmentType GetEquipmentType() => equipmentType;
    public int GetBonusHP() => bonusHP;
    public int GetBonusAttack() => bonusAttack;
    public int GetBonusDefense() => bonusDefense;
    public List<AdventurerTraitSO> GetGrantedTraits() => grantedTraits;
}