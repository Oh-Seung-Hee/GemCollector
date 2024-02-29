using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject itemInfoUI;

    // 아이템에서 마우스를 치웠을 때
    public void OnPointerExit(PointerEventData _eventData)
    {
        itemInfoUI.SetActive(false);
    }

    // 아이템에 마우스를 올렸을 때
    public void OnPointerEnter(PointerEventData _eventData)
    {
        itemInfoUI.SetActive(true);
    }
}
