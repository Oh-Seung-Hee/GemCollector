using UnityEngine;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;

    private PlayerStats playerStats;
    private CharacterHealth characterHealth;

    public static Inventory instance;

    void Awake()
    {
        // 싱글톤
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        characterHealth = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
    }

    private void Start()
    {
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
    }

    // 인벤토리에 아이템 추가
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
    }

    // 획득한 아이템이 기존에 획득했던 Gem인지 확인
    ItemSlot GetItemStack(ItemData _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == _item && slots[i].quantity < _item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    // 비어있는 슬롯 확인
    public ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    // UI 업데이트
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

    // 아이템 선택
    public void SelectItem(int _index)
    {
        selectedItem = slots[_index];
        selectedItemIndex = _index;

        uiSlots[_index].UpdateItemInfo(selectedItem.item.displayName, selectedItem.item.description);
    }

    // 아이템 사용
    public void OnUseButton()
    {
        if (selectedItem.item.type == ItemType.Expendable)
        {
            for (int i = 0; i < selectedItem.item.expendables.Length; i++)
            {
                switch (selectedItem.item.expendables[i].type)
                {
                    case ExpendableType.SpeedUp:
                        playerStats.MoveSpeed = playerStats.MoveSpeed * selectedItem.item.expendables[i].value; break;
                    case ExpendableType.PowerUp:
                        playerStats.AttackDamage = playerStats.AttackDamage * selectedItem.item.expendables[i].value; break;
                    case ExpendableType.Heal:
                        characterHealth.health += characterHealth.maxHealth * selectedItem.item.expendables[i].value; break;
                }
            }

            RemoveSelectedItem();
        }
    }

    // 아이템 제거
    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;

        if (selectedItem.quantity <= 0)
        {
            selectedItem.item = null;
        }

        UpdateUI();
    }
}