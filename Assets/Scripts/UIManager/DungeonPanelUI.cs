using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class DungeonPanelUI : MonoBehaviour
{
    [SerializeField] private Transform dungeonListContainer;
    [SerializeField] private GameObject dungeonSelectButtonPrefab;
    [SerializeField] private Transform adventurerListContainer;
    [SerializeField] private GameObject adventurerSelectButtonPrefab;
    [SerializeField] private Button startSimulationButton;


    private DungeonAreaSO selectedDungeon;
    private DungeonSelectButtonPrefab selectedDungeonPrefab;
    private AdventurerDataSO selectedAdventurer;

    private Dictionary<DungeonAreaSO, List<AdventurerDataSO>> dungeonAssignments = new();

    private List<DungeonAreaSO> availableDungeons;
    private List<AdventurerDataSO> availableAdventurers;


    private void OnEnable() {
        Initialize(DungeonManager.Instance.GetAvailableDungeons(), DungeonManager.Instance.GetAvailableAdventurers());
    }

    public void Initialize(List<DungeonAreaSO> dungeons, List<AdventurerDataSO> adventurers) {
        availableDungeons = dungeons;
        availableAdventurers = adventurers;
        dungeonAssignments.Clear();

        foreach (var dungeon in dungeons)
            dungeonAssignments[dungeon] = new List<AdventurerDataSO>();

        LoadDungeonList();
        startSimulationButton.onClick.AddListener(OnStartSimulation);
    }
    private void LoadDungeonList() {
        foreach (Transform child in dungeonListContainer)
            Destroy(child.gameObject);

        foreach (var dungeon in availableDungeons) {
            var prefab = Instantiate(dungeonSelectButtonPrefab, dungeonListContainer);
            
            var entryUI = prefab.GetComponent<DungeonSelectButtonPrefab>();
            var btn = entryUI.GetButton();
            entryUI.SetButtonText(dungeon.GetAreaName());
            btn.onClick.AddListener(() => OnSelectDungeon(dungeon, entryUI));
            entryUI.RefreshAssignedAdventurers(dungeonAssignments[dungeon]);
            
        }
    }



    private void OnSelectDungeon(DungeonAreaSO dungeon, DungeonSelectButtonPrefab prefab) {
        selectedDungeon = dungeon;
        selectedDungeonPrefab = prefab;
        LoadAdventurerList();
    }

    private void LoadAdventurerList() {
        foreach (Transform child in adventurerListContainer)
            Destroy(child.gameObject);

        var assigned = dungeonAssignments[selectedDungeon];

        foreach (var adventurer in availableAdventurers) {
            bool isAssigned = assigned.Contains(adventurer);

            var entryGO = Instantiate(adventurerSelectButtonPrefab, adventurerListContainer);
            var entryUI = entryGO.GetComponent<Dungeon_AdventurerSelectButtonPrefab>();

            entryUI.Initialize(
                adventurer,
                isAssigned,
                OnAssignAdventurer,
                OnUnassignAdventurer
            );
        }
    }

    private void OnAssignAdventurer(AdventurerDataSO adventurer) {
        if (selectedDungeon == null || adventurer == null) return;

        var assigned = dungeonAssignments[selectedDungeon];
        if (!assigned.Contains(adventurer)) {
            assigned.Add(adventurer);
            LoadAdventurerList();
            selectedDungeonPrefab.RefreshAssignedAdventurers(assigned);
        }
    }

    private void OnUnassignAdventurer(AdventurerDataSO adventurer) {
        if (selectedDungeon == null || adventurer == null) return;

        var assigned = dungeonAssignments[selectedDungeon];
        if (assigned.Contains(adventurer)) {
            assigned.Remove(adventurer);
            LoadAdventurerList();
            selectedDungeonPrefab.RefreshAssignedAdventurers(assigned);
        }
    }

    private void OnStartSimulation() {
        DungeonManager.Instance.StartDungeonPhase(dungeonAssignments);
    }

}
