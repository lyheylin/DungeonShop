using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIconWithTooltipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private TMP_Text tooltipText;

    private ItemDataSO currentItem;
    private LootItemDataSO currentLootItem;

    public void SetItem(ItemDataSO itemData) {
        currentItem = itemData;
        iconImage.sprite = itemData.GetIcon(); 
        tooltipText.text = GetTooltipText(itemData);
    }

    public void SetLootItem(LootItemDataSO itemData) {
        currentLootItem = itemData;
        iconImage.sprite = itemData.GetIcon();
        tooltipText.text = GetTooltipText(itemData);
    }

    private string GetTooltipText(ItemDataSO item) {
        return $"<b>{item.GetName()}</b>\n{item.GetDescription()}";
    }

    private string GetTooltipText(LootItemDataSO item) {
        return $"<b>{item.GetName()}</b>\n{item.GetDescription()}";
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.SetActive(false);
    }
}