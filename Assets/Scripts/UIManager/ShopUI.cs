using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private GameObject shopSlotPrefab;
    [SerializeField] private Transform gridRoot;
    [SerializeField] private Button openShopButton;

    private void Start() {
        openShopButton.onClick.AddListener(OnOpenShopButtonClicked);
    }
    private void OnEnable() {
        RefreshShopSlots();
        openShopButton.gameObject.SetActive(true); //?
        if (GameManager.Instance != null) {
        }
    }
    private void OnDisable() {
        openShopButton.gameObject.SetActive(false);
        if (GameManager.Instance != null) {
        }
    }

    //PH
    private const int MaxShopSlots = 6; // Expandable later
    private List<ShopSlot> sellingSlots = new List<ShopSlot>();

    public void Initialize() {
        RefreshShopSlots();
        openShopButton.gameObject.SetActive(true);
    }
    public void RefreshShopSlots() {
        sellingSlots = new List<ShopSlot>();
        foreach (Transform child in gridRoot)
            Destroy(child.gameObject);

        int slotsFilled = 0;
        foreach(var item in ShopManager.Instance.GetShopItems()) {
            GameObject slotGO = Instantiate(shopSlotPrefab, gridRoot);
            ShopSlot slot = slotGO.GetComponent<ShopSlot>();
            slot.Initialize(item.GetItemData(), item.GetQuantity(), item.GetPrice());

            sellingSlots.Add(slot);
            slotsFilled++;
        }
    }
    private void OnOpenShopButtonClicked() {
        ShopManager.Instance.StartShopPhase();
        openShopButton.gameObject.SetActive(false); 
    }
}
