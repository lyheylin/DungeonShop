using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
public class ItemDatabase : ScriptableObject {
    [SerializeField] private List<ItemDataSO> allItems;

    private Dictionary<string, ItemDataSO> itemLookup;

    private void OnEnable() {
        if (allItems == null)
            Debug.LogWarning($"Item database not initialized.");

        itemLookup = new Dictionary<string, ItemDataSO>();

        foreach (var item in allItems) {
            if (!itemLookup.ContainsKey(item.GetName())) {
                itemLookup.Add(item.GetName(), item);
            } else {
                Debug.LogWarning($"Duplicate item name found: {item.GetName()}");
            }
        }
    }

    public ItemDataSO GetItemByName(string name) {
        if (itemLookup == null || !itemLookup.ContainsKey(name)) {
            Debug.LogWarning($"Item not found in database: {name}");
            return null;
        }

        return itemLookup[name];
    }

    public List<ItemDataSO> GetAllItems() => allItems;
    public void AddItem(ItemDataSO item) {
        allItems.Add(item);
    }
}