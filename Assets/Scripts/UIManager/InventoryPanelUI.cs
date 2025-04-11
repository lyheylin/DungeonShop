using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class InventoryPanelUI : MonoBehaviour {
    [SerializeField] private GameObject itemEntryPrefab;
    [SerializeField] private Transform contentRoot;

    private void OnEnable() {
        if (DayCycleManager.Instance != null) {
            DayCycleManager.Instance.OnDayStarted += Handle_OnDayStarted;
            Debug.Log("LISTENING!");
        }
    }
    private void OnDisable() {
        if (DayCycleManager.Instance != null) {
            DayCycleManager.Instance.OnDayStarted -= Handle_OnDayStarted;
        }
    }

    private void Handle_OnDayStarted(int obj) {
        RefreshInventory();
    }

    public void RefreshInventory() {
        Debug.Log("Refresh Inventory");
        foreach (Transform child in contentRoot)
            Destroy(child.gameObject);

        foreach (var item in Inventory.Instance.GetItems()) {
            GameObject entry = Instantiate(itemEntryPrefab, contentRoot);
            TMP_Text text = entry.GetComponentInChildren<TMP_Text>();
            text.text = $"{item.GetItemData().GetName()} x{item.GetQuantity()}";
        }
    }
}