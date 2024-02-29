using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    //public TextMeshProUGUI quatityText;
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
        //quatityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.instance.SelectItem(index);
    }
}