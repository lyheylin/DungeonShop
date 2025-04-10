using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CraftingManager : MonoBehaviour {
    public static CraftingManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool CanCraftRecipe(RecipeDataSO recipe) {
        foreach (var ingredient in recipe.GetIngredients()) {
            int playerAmount = Inventory.Instance.GetItemQuantity(ingredient.item);
            if (playerAmount < ingredient.quantity)
                return false;
        }
        return true;
    }

    public bool CraftRecipe(RecipeDataSO recipe) {
        if (!CanCraftRecipe(recipe)) {
            Debug.Log("Cannot craft recipe: Not enough ingredients.");
            return false;
        }

        foreach (var ingredient in recipe.GetIngredients()) {
            Inventory.Instance.RemoveItem(ingredient.item, ingredient.quantity);
        }

        Inventory.Instance.AddItem(recipe.GetResult(), recipe.GetResultAmount());

        Debug.Log($"Crafted {recipe.GetResultAmount()} x {recipe.GetResult().GetName()}");
        return true;
    }   
}