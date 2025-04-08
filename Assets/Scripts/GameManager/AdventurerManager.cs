using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerManager : MonoBehaviour {
    public static AdventurerManager Instance { get; private set; }
    [SerializeField] private List<AdventurerDataSO> adventurers;
    private List<Adventurer> activeAdventurers = new List<Adventurer>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAdventurers();
    }

    public void InitializeAdventurers() {
        activeAdventurers.Clear();

        foreach (var adventurer in adventurers) {
            Adventurer newAdventurer = new Adventurer(adventurer);
            activeAdventurers.Add(newAdventurer);
        }

        Debug.Log($"Initialized {activeAdventurers.Count} adventurers.");
    }

    public List<Adventurer> GetActiveAdventurers() {
        return activeAdventurers;
    }
}
