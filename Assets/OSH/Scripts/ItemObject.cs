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
            Destroy(gameObject);
        }
    }

    // 아이템이 플레이어와 닿았을 때
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            UIManager.instance.ShowEventTextPopup();
            AudioManager.instance.PlaySFX(AudioManager.instance.getItemClip);
            OnInteract();
        }
    }
}