using Codice.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LootItemSOGenerator{
    

    [MenuItem("Tools/SOGenerator/Generate Loot Items")]  
    public static void GenerateItems() {
        string lootItemPath = "Assets/ScriptableObjects/lootItems/";
        string itemPath = "Assets/ScriptableObjects/inventoryItems/";
        string databasePath = "Assets/ScriptableObjects/database/";
        //TODO: Read from json file
        List<(string fileName, string lootName, string description, int rarity)> lootItems = new() {
            ("SlimeJelly", "Slime Jelly", "The innards of slimes.", 0),
            ("GoblinEar", "Goblin Ear", "Ears of goblins, gross.", 0),
            ("GrossMeat", "Gross Meat", "Meat of zombies, smelly and slimy.", 0),
            ("BatWing", "Bat Wings", "Wings of bats.", 0)
        };

        //Loot items will all have corresponding inventory items.
        //Needs to match, TODO: generate these by code later.
        List<(string fileName, string itemName, string description, ItemType itemType, bool isSellable, int basePrice)> invItems = new() {
            ("SlimeJelly", "Slime Jelly", "Clear, chunky jelly that can be used for lotion, possesses medicinal properties.", ItemType.Material, false, 0),
            ("GoblinEar", "Goblin Ear", "The accumulated waxy substance coated on the ears of Goblins.", ItemType.Material, false ,0 ),
            ("GrossMeat", "Gross Meat", "Specific bacteria and fungi can be collected from these.", ItemType.Material, false, 0),
            ("BatWing", "Bat Wings", "Tough but flexible leather.", ItemType.Material, false, 0),

            //Pure inventory items
            ("mHPPotion", "Minor Healing Potion", "A bottle of potion that heal wounds slightly.", ItemType.Consumable, true, 15)

        };

        foreach (var (fileName, lootName, description, rarity) in lootItems) {
            LootItemDataSO item = ScriptableObject.CreateInstance<LootItemDataSO>();

            //TODO: Assign placeholder icon

            SerializedObject so = new(item);
            so.FindProperty("lootName").stringValue = lootName;
            so.FindProperty("description").stringValue = description;
            so.FindProperty("rarity").intValue = rarity;
            so.ApplyModifiedProperties();

            AssetDatabase.CreateAsset(item, $"{lootItemPath}{fileName}.asset");
        }

        ItemDatabase database = ScriptableObject.CreateInstance<ItemDatabase>();
        SerializedObject db = new(database);
        var itemList = db.FindProperty("allItems");

        foreach (var (fileName,  itemName,  description,  itemType,  isSellable,  basePrice) in invItems) {
            ItemDataSO item = ScriptableObject.CreateInstance<ItemDataSO>();

            SerializedObject so = new(item);
            so.FindProperty("itemName").stringValue = itemName;
            so.FindProperty("description").stringValue = description;
            so.FindProperty("itemType").intValue = (int)itemType;
            so.FindProperty("isSellable").boolValue = isSellable;
            so.FindProperty("basePrice").intValue = basePrice;
            so.ApplyModifiedProperties();

            
            AssetDatabase.CreateAsset(item, $"{itemPath}{fileName}.asset");


            itemList.arraySize++;
            itemList.GetArrayElementAtIndex(itemList.arraySize-1).objectReferenceValue = item;
        }
        db.ApplyModifiedProperties();
        AssetDatabase.CreateAsset(database, $"{databasePath}ItemDatabase.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Loot items created!");
    }

    [MenuItem("Tools/SOGenerator/Generate Recipes")]
    public static void GenerateRecipes() {
        string itemPath = "Assets/ScriptableObjects/inventoryItems/";
        string path = "Assets/ScriptableObjects/recipes/";

        // Load required items
        ItemDataSO slimeJelly = AssetDatabase.LoadAssetAtPath<ItemDataSO>($"{itemPath}SlimeJelly.asset");
        ItemDataSO grossMeat = AssetDatabase.LoadAssetAtPath<ItemDataSO>($"{itemPath}GrossMeat.asset");
        ItemDataSO potion = AssetDatabase.LoadAssetAtPath<ItemDataSO>($"{itemPath}mHPPotion.asset");

        if (slimeJelly == null || potion == null) {
            Debug.LogError("Required items not found. Please generate them first.");
            return;
        }

        RecipeDataSO recipe = ScriptableObject.CreateInstance<RecipeDataSO>();
        SerializedObject so = new(recipe);

        so.FindProperty("recipeName").stringValue = "Minor Healing Potion";

        var ingredientsProp = so.FindProperty("ingredients");
        ingredientsProp.arraySize = 2;

        ingredientsProp.GetArrayElementAtIndex(0).FindPropertyRelative("item").objectReferenceValue = slimeJelly;
        ingredientsProp.GetArrayElementAtIndex(0).FindPropertyRelative("quantity").intValue = 2;

        ingredientsProp.GetArrayElementAtIndex(1).FindPropertyRelative("item").objectReferenceValue = grossMeat;
        ingredientsProp.GetArrayElementAtIndex(1).FindPropertyRelative("quantity").intValue = 1;

        so.FindProperty("result").objectReferenceValue = potion;
        so.FindProperty("resultAmount").intValue = 3;

        so.ApplyModifiedProperties();

        AssetDatabase.CreateAsset(recipe, $"{path}mHPPotion.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Sample Recipe created!");
    }
}
