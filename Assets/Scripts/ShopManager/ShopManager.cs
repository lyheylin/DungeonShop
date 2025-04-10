using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public static ShopManager Instance { get; private set; }

    [SerializeField] private int maxShopSlots = 5;
    private List<ShopSlot> shopSlots = new();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<ShopSlot> GetShopSlots() => shopSlots;
    public int GetMaxShopSlots() => maxShopSlots;

    public void AddItemToShop(ItemDataSO item, int quantity, int price) {
        if (shopSlots.Count >= maxShopSlots) return;
        if (!item.IsSellable()) return;

        shopSlots.Add(new ShopSlot(item, quantity, price));
    }

    public void ClearShopSlots() {
        shopSlots.Clear();
    }
}