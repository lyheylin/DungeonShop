using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class DungeonSelectButtonPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text assignedAdventurers;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;

    public void RefreshAssignedAdventurers(List<AdventurerDataSO> assignedAdventurers) {
        string text = "";
        foreach(var adventurer in assignedAdventurers) {
            text += adventurer.GetAdventurerName() + "\n";
        }
       this.assignedAdventurers.text = text;
    }

    public void SetButtonText(string text) {
        buttonText.text = text;
    }

    public Button GetButton() => button;
}
