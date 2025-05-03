using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanelUI : MonoBehaviour{

    private void OnEnable() {
        if (GameManager.Instance != null) {
            //GameManager.Instance.OnResultStateStarted += OnResultStateStarted;
        }
        ResultManager.Instance.SetupResults(DungeonManager.Instance.GetAvailableAdventurers(), ShopManager.Instance.GetGold());
    }

    private void OnResultStateStarted(GameState obj) {
        ResultManager.Instance.SetupResults(DungeonManager.Instance.GetAvailableAdventurers(), ShopManager.Instance.GetGold());
        Debug.Log("listing results.");
    }

    private void OnDisable() {
        if (GameManager.Instance != null) {
            //GameManager.Instance.OnResultStateStarted -= OnResultStateStarted;
        }
    }

}
