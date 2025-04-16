using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public static ShopManager Instance { get; private set; }

    [SerializeField] private int maxShopSlots = 5;
    [SerializeField] private int gold = 0;
    [SerializeField] private ShopUI shopUI;
    private List<ShopItem> shopItems = new();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    //Shop slot management
    public IReadOnlyList<ShopItem> GetShopItems() => shopItems.AsReadOnly();
    public int GetMaxShopSlots() => maxShopSlots;

    public void AddItemToShop(ItemDataSO item, int quantity, int price) {
        if (shopItems.Count >= maxShopSlots) return;
        if (!item.IsSellable()) return;

        shopItems.Add(new ShopItem(item, quantity, price));
        //Inventory.Instance.RemoveItem(item, 1); // Move from inventory double remove
        LogManager.Instance.Log($"{item.GetName()} moved to shop at {price} gold.");
    }

    public void RemoveItemFromShop(ShopItem item, int quantity) {
        //TODO
        if (quantity > item.GetQuantity()) {
            Debug.Log("Not enough items in shop!");
            return;
        }

        item.RemoveItem(quantity);
        if(item.GetQuantity()==0)
            shopItems.Remove(item);
        //Inventory.Instance.AddItem(item.GetItemData(), 1); // Return to inventory?
    }


    public void ClearShopSlots() {
        shopItems.Clear();
    }

    //Shop phase logic
    public void StartShopPhase() {
        List<Adventurer> visitingAdventurers;
        Debug.Log("Shop phase started!");
        visitingAdventurers = AdventurerManager.Instance.GetActiveAdventurers(); // Assume this exists
        //shopDisplay.RefreshShopSlots();
        StartCoroutine(HandleShopVisits(visitingAdventurers));
    }

    private System.Collections.IEnumerator HandleShopVisits(List<Adventurer> visitingAdventurers) {
        foreach (var adventurer in visitingAdventurers) {
            yield return new WaitForSeconds(1f); // short delay for pacing
            TrySellToAdventurer(adventurer.GetAdventurerDataSO());
        }

        Debug.Log("Shop phase ended!");
    }

    private void TrySellToAdventurer(AdventurerDataSO adventurer) {
        if (shopItems.Count == 0) return;

        ShopItem randomItem = shopItems[Random.Range(0, shopItems.Count)];

        if (adventurer.WillBuyItem(randomItem)) {
            LogManager.Instance.Log($"{adventurer.GetAdventurerName()} bought {randomItem.GetItemData().GetName()} for {randomItem.GetPrice()} gold!");
            gold += randomItem.GetPrice();

            adventurer.AddToInventory(randomItem.GetItemData(), 1);

            RemoveItemFromShop(randomItem, 1);
            shopUI.RefreshShopSlots();
        } else {
            LogManager.Instance.Log($"{adventurer.GetAdventurerName()} thought {randomItem.GetItemData().GetName()} was too expensive.");
        }
    }
}