using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon_AdventurerSelectButtonPrefab : MonoBehaviour{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button assignButton;
    [SerializeField] private Button unassignButton;

    private AdventurerDataSO adventurer;
    private Action<AdventurerDataSO> onAssign;
    private Action<AdventurerDataSO> onUnassign;

    public void Initialize(
        AdventurerDataSO data,
        bool isAssigned,
        Action<AdventurerDataSO> onAssignClicked,
        Action<AdventurerDataSO> onUnassignClicked) {
        adventurer = data;
        nameText.text = data.GetAdventurerName();

        assignButton.gameObject.SetActive(!isAssigned);
        unassignButton.gameObject.SetActive(isAssigned);

        onAssign = onAssignClicked;
        onUnassign = onUnassignClicked;

        assignButton.onClick.AddListener(() => onAssign?.Invoke(adventurer));
        unassignButton.onClick.AddListener(() => onUnassign?.Invoke(adventurer));
    }
}
