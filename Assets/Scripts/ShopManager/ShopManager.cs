using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public static ShopManager Instance { get; private set; }

    [SerializeField] private int maxShopSlots = 5;
    [SerializeField] private int gold = 0;
    private List<ShopSlot> shopSlots = new();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    //Shop slot management
    public IReadOnlyList<ShopSlot> GetShopSlots() => shopSlots.AsReadOnly();
    public int GetMaxShopSlots() => maxShopSlots;

    public void AddItemToShop(ItemDataSO item, int quantity, int price) {
        if (shopSlots.Count >= maxShopSlots) return;
        if (!item.IsSellable()) return;

        shopSlots.Add(new ShopSlot(item, quantity, price));
        //Inventory.Instance.RemoveItem(item, 1); // Move from inventory double remove
        Debug.Log($"{item.GetName()} moved to shop at {price} gold.");
    }

    public void RemoveItemFromShop(ShopSlot item) {
        //TODO
        shopSlots.Remove(item);
        //Inventory.Instance.AddItem(item.GetItemData(), 1); // Return to inventory?
    }


    public void ClearShopSlots() {
        shopSlots.Clear();
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
        GameManager.Instance.ChangeState(GameState.Dungeon);
    }

    private void TrySellToAdventurer(AdventurerDataSO adventurer) {
        if (shopSlots.Count == 0) return;

        ShopSlot randomItem = shopSlots[Random.Range(0, shopSlots.Count)];
        int acceptablePrice = randomItem.GetItemData().GetBasePrice() * 2;

        if (randomItem.price <= acceptablePrice) {
            Debug.Log($"{adventurer.GetAdventurerName()} bought {randomItem.GetItemData().GetName()} for {randomItem.price} gold!");
            gold += randomItem.price;
            shopSlots.Remove(randomItem);
            //shopDisplay.RefreshShopSlots();
        } else {
            Debug.Log($"{adventurer.GetAdventurerName()} thought {randomItem.GetItemData().GetName()} was too expensive.");
        }
    }
}