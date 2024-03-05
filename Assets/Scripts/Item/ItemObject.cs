using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public void OnInteract()
    {
        if (Inventory.instance.GetEmptySlot() != null)
        {
            Inventory.instance.AddItem(item);
            UIManager.instance.ShowEventTextPopup();
            Destroy(gameObject);
        }
    }

    // 아이템이 플레이어와 닿았을 때
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.getItemClip);
            OnInteract();
        }

        if (gameObject.CompareTag("Gem"))
        {
            // 5개 종류 다 획득 시
            if (Inventory.instance.clearCondition == 0)
            {
                UIManager.instance.ShowPopup("ClearPopup");
            }
        }
    }
}