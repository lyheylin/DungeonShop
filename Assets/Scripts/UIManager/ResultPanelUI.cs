using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelUI : MonoBehaviour {
    [SerializeField] private Transform resultListParent;
    [SerializeField] private AdventurerLootEntryUI advLootEntryPrefab;
    [SerializeField] private LootEntryUI lootEntryPrefab;
    [SerializeField] private TMP_Text totalGoldText;
    [SerializeField] private TMP_Text availableGoldText;
    [SerializeField] private Button confirmPurchaseButton;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.red;

    private void OnEnable() {
        var adventurers = DungeonManager.Instance.GetAvailableAdventurers();
        ResultManager.Instance.InitializeResults(adventurers);

        PopulateUI();
        UpdateGoldDisplay();
        confirmPurchaseButton.onClick.AddListener(OnConfirmClicked);
    }

    private void PopulateUI() {
        foreach (Transform child in resultListParent)
            Destroy(child.gameObject);

        foreach (var adventurer in ResultManager.Instance.GetAdventurers()) {
            var advUI = Instantiate(advLootEntryPrefab, resultListParent);
            advUI.Setup(adventurer);

            foreach (var loot in ResultManager.Instance.GetLootForAdventurer(adventurer)) {
                var lootUI = Instantiate(lootEntryPrefab, advUI.GetContainer());
                lootUI.Setup(this, loot.item, adventurer, loot.pricePerUnit, loot.maxQuantity);
            }
        }
    }

    public void NotifyQuantityChanged(AdventurerDataSO owner, LootItemDataSO item, int quantity) {
        ResultManager.Instance.SetSelectedQuantity(owner, item, quantity);
        UpdateGoldDisplay();
    }

    private void UpdateGoldDisplay() {
        int totalCost = ResultManager.Instance.GetTotalCost();
        int gold = ResultManager.Instance.GetAvailableGold();
        bool canAfford = ResultManager.Instance.CanAfford();

        totalGoldText.text = $"Total: {totalCost}g";
        availableGoldText.text = $"Gold: {ShopManager.Instance.GetGold()}g";

        totalGoldText.color = canAfford ? normalColor : warningColor;
        availableGoldText.color = canAfford ? normalColor : warningColor;

        confirmPurchaseButton.interactable = canAfford;

        var canvasGroup = confirmPurchaseButton.GetComponent<CanvasGroup>() ??
                          confirmPurchaseButton.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = canAfford ? 1f : 0.5f;
        canvasGroup.interactable = canAfford;
        canvasGroup.blocksRaycasts = canAfford;
    }

    public void OnConfirmClicked() {
        ResultManager.Instance.ConfirmPurchases();

        PopulateUI();
        UpdateGoldDisplay();
        // Possibly transition to the next phase
    }
}
