using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeListUI : MonoBehaviour {
    [SerializeField] private GameObject recipeButtonPrefab;
    [SerializeField] private Transform contentRoot;
    [SerializeField] private InventoryPanelUI inventoryUI;
    [SerializeField] private SellingSlotsDisplayUI shopDisplayUI;

    [SerializeField] private List<RecipeDataSO> availableRecipes;

    private void Start() {
        foreach (var recipe in availableRecipes) {
            GameObject buttonGO = Instantiate(recipeButtonPrefab, contentRoot);
            TMP_Text text = buttonGO.GetComponentInChildren<TMP_Text>();
            text.text = recipe.GetRecipeName();

            Button button = buttonGO.GetComponent<Button>();
            button.onClick.AddListener(() => {
                bool success = CraftingManager.Instance.CraftRecipe(recipe);
                if (success) {
                    LogManager.Instance.Log($"Crafted {recipe.GetResultAmount()} {recipe.GetRecipeName()}");
                    inventoryUI.RefreshInventory();
                    shopDisplayUI.RefreshShopSlots();
                }
            });
        }
    }
}