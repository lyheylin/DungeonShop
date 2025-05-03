using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdventurerLootEntryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text adventurerNameText;
    [SerializeField] private Transform lootEntryContainer;
    private AdventurerDataSO adventurerData;

    public AdventurerDataSO GetAdventurerDataSO() => adventurerData;

    public void Setup(AdventurerDataSO adventurer) {
        adventurerData = adventurer;
        adventurerNameText.text = adventurer.GetAdventurerName();
    }

    public Transform GetContainer() {
        return lootEntryContainer;
    }
}
