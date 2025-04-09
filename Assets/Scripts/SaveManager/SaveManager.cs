using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    public static SaveManager Instance { get; private set; }

    [SerializeField] private ItemDatabase itemDatabase;
    private string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    private class GameSaveData {
        public InventorySaveData inventory;
        // future: public AdventurerSaveData adventurers; etc.
    }

    public void SaveGame() {
        var data = new GameSaveData {
            inventory = Inventory.Instance.GetSaveData()
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Game saved to {savePath}");
    }

    public void LoadGame() {
        if (!File.Exists(savePath)) {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        var data = JsonUtility.FromJson<GameSaveData>(json);

        Inventory.Instance.LoadFromSaveData(data.inventory, itemDatabase);
        Debug.Log("Game loaded.");
    }
}