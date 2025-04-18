using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventurerViewerUI : MonoBehaviour {
    [SerializeField] private Transform adventurerButtonContainer;
    [SerializeField] private GameObject adventurerButtonPrefab;
    [SerializeField] private Transform inventoryGridContainer;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Transform equippedItemDisplay;

    private List<AdventurerDataSO> adventurers = new();

    public void Initialize() {
        foreach (Adventurer adventurer in AdventurerManager.Instance.GetActiveAdventurers()) {
            adventurers.Add(adventurer.GetAdventurerDataSO());
        }

        foreach (Transform child in adventurerButtonContainer)
            Destroy(child.gameObject);

        foreach (var adv in adventurers) {
            var buttonObj = Instantiate(adventurerButtonPrefab, adventurerButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = adv.GetAdventurerName();
            buttonObj.GetComponent<Button>().onClick.AddListener(() => ShowAdventurerDetails(adv));
        }
    }

    private void ShowAdventurerDetails(AdventurerDataSO adventurer) {
        foreach (Transform child in inventoryGridContainer)
            Destroy(child.gameObject);
        foreach (Transform child in equippedItemDisplay)
            Destroy(child.gameObject);


        foreach (var item in adventurer.GetInventory()) {
            var slot = Instantiate(inventorySlotPrefab, inventoryGridContainer);
            slot.GetComponentInChildren<Image>().sprite = item.ItemData.GetIcon();
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item.Quantity.ToString();
        }

        if (adventurer.GetEquippedItem() != null) {
            var equipped = Instantiate(inventorySlotPrefab, equippedItemDisplay);
            equipped.GetComponentInChildren<Image>().sprite = adventurer.GetEquippedItem().GetIcon();
            equipped.GetComponentInChildren<TextMeshProUGUI>().text = adventurer.GetEquippedItem().GetName();
        }
    }
}