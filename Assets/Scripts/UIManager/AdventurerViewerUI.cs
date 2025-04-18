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
    [SerializeField] private Image equippedItemIcon;
    [SerializeField] private TextMeshProUGUI equippedItemName;

    private List<AdventurerDataSO> adventurers;

    public void Initialize(List<AdventurerDataSO> adventurerList) {
        adventurers = adventurerList;

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

        foreach (var item in adventurer.GetInventory()) {
            var slot = Instantiate(inventorySlotPrefab, inventoryGridContainer);
            slot.GetComponentInChildren<Image>().sprite = item.ItemData.GetIcon();
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item.Quantity.ToString();
        }

        if (adventurer.GetEquippedItem() != null) {
            equippedItemIcon.sprite = adventurer.GetEquippedItem().GetIcon();
            equippedItemName.text = adventurer.GetEquippedItem().GetName();
        } else {
            equippedItemIcon.sprite = null;
            equippedItemName.text = "None";
        }
    }
}