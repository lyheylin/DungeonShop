using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    public static SaveManager Instance { get; private set; }

    [SerializeField] private ItemDatabase itemDatabase;

    public event Action<GameSaveData> OnDataLoaded;

    private string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SaveGame() {
        var data = new GameSaveData {
            inventory = Inventory.Instance.GetSaveData(),
            time = DayCycleManager.Instance.GetSaveData(),
            adventurers = AdventurerManager.Instance.GetSaveData(), // You'll implement this
            shop = ShopManager.Instance.GetSaveData()              // You'll implement this
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
        DayCycleManager.Instance.LoadFromSaveData(data.time);
        AdventurerManager.Instance.LoadFromSaveData(data.adventurers, itemDatabase);
        ShopManager.Instance.LoadFromSaveData(data.shop, itemDatabase);


        OnDataLoaded?.Invoke(data);
        Debug.Log("Game loaded.");
    }
}