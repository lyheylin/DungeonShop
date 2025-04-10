using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
public class RecipeDataSO : ScriptableObject {
    [Serializable]
    public class Ingredient {
        public ItemDataSO item;
        public int quantity;
    }

    [SerializeField] private string recipeName;
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private ItemDataSO result;
    [SerializeField] private int resultAmount = 1;

    public string GetRecipeName() => recipeName;
    public List<Ingredient> GetIngredients() => ingredients;
    public ItemDataSO GetResult() => result;
    public int GetResultAmount() => resultAmount;
}