using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    //public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public GameObject useButton;

    //private PlayerController controller;
    //private PlayerConditions condition;

    [Header("Events")]
    //public UnityEvent onOpenInventory;
    //public UnityEvent onCloseInventory;

    public static Inventory instance;

    void Awake()
    {
        // 싱글톤
        instance = this;
        //controller = GetComponent<PlayerController>();
        //condition = GetComponent<PlayerConditions>();
    }

    private void Start()
    {
        //inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

        //ClearSeletecItemWindow();
    }

    public void AddItem(ItemData _item)
    {
        // 아이템이 Gem일 때
        if (_item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(_item);
            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = _item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        ThrowItem(_item);
    }

    ItemSlot GetItemStack(ItemData _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == _item && slots[i].quantity < _item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                uiSlots[i].Set(slots[i]);
            }
            else
            {
                uiSlots[i].Clear();
            }
        }
    }

    void ThrowItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360f));
    }

    public void SelectItem(int _index)
    {
        if (slots[_index].item == null)
            return;

        selectedItem = slots[_index];
        selectedItemIndex = _index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        useButton.SetActive(selectedItem.item.type == ItemType.Expendable);
    }

    public void OnUseButton()
    {
        if (selectedItem.item.type == ItemType.Expendable)
        {
            for (int i = 0; i < selectedItem.item.expendables.Length; i++)
            {
                switch (selectedItem.item.expendables[i].type)
                {
                    //case ExpendableType.SpeedUp:
                    //    condition.Heal(selectedItem.item.expendables[i].value); break;
                    //case ExpendableType.PowerUp:
                    //    condition.Eat(selectedItem.item.expendables[i].value); break;
                }
            }
        }
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            selectedItem.item = null;
            //ClearSeletecItemWindow();
        }

        UpdateUI();
    }


    //public void OnInventoryButton(InputAction.CallbackContext _callbackContext)
    //{
    //    if (_callbackContext.phase == InputActionPhase.Started)
    //    {
    //        Toggle();
    //    }
    //}

    //public void Toggle()
    //{
    //    if (inventoryWindow.activeInHierarchy)
    //    {
    //        inventoryWindow.SetActive(false);
    //        onCloseInventory?.Invoke();
    //        //controller.ToggleCursor(false);
    //    }
    //    else
    //    {
    //        inventoryWindow.SetActive(true);
    //        onOpenInventory?.Invoke();
    //        //controller.ToggleCursor(true);
    //    }
    //}

    //public bool IsOpen()
    //{
    //    return inventoryWindow.activeInHierarchy;
    //}

    //private void ClearSeletecItemWindow()
    //{
    //    selectedItem = null;
    //    selectedItemName.text = string.Empty;
    //    selectedItemDescription.text = string.Empty;

    //    //selectedItemStatNames.text = string.Empty;
    //    //selectedItemStatValues.text = string.Empty;

    //    useButton.SetActive(false);
    //    //equipButton.SetActive(false);
    //    //unEquipButton.SetActive(false);
    //    //dropButton.SetActive(false);
    //}

    //public void RemoveItem(ItemData item)
    //{
    //}

    //public bool HasItems(ItemData item, int quantity)
    //{
    //    return false;
    //}

    ////public void OnDropButton()
    ////{
    ////    ThrowItem(selectedItem.item);
    ////    RemoveSelectedItem();
    ////}
}