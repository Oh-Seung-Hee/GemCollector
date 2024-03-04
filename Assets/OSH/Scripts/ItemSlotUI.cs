using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private GameObject itemInfoUI;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    public Button button;
    public Image icon;
    private ItemSlot curSlot;

    public int index;

    public void Set(ItemSlot _slot)
    {
        curSlot = _slot;
        icon.gameObject.SetActive(true);
        icon.sprite = _slot.item.icon;
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
    }

    // 아이템에서 마우스를 치웠을 때
    public void OnPointerExit(PointerEventData _eventData)
    {
        itemInfoUI.SetActive(false);
    }

    // 아이템에 마우스를 올렸을 때
    public void OnPointerEnter(PointerEventData _eventData)
    {
        // 아이템이 존재하지 않을 때
        if (Inventory.instance.slots[index].item == null)
            return;

        Inventory.instance.SelectItem(index);
        itemInfoUI.SetActive(true);
    }

    // 아이템 설명창 업데이트
    public void UpdateItemInfo(string _displayName, string _description)
    {
        selectedItemName.text = _displayName;
        selectedItemDescription.text = _description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 아이템이 존재하지 않을 때
        if (Inventory.instance.slots[index].item == null)
            return;

        Inventory.instance.OnUseButton();
    }
}