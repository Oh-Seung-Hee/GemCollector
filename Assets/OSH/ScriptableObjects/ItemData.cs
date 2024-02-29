using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Expendable, // 소모형 아이템
    Collectible // 수집형 아이템(Gem)
}

public enum ExpendableType
{
    MultiplyReward,
    SpeedUp,
    PowerUp
}

[System.Serializable]
public class ItemDataExpendable
{
    public ExpendableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    //[Header("Stat")]
    //public float value;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Expendable")]
    public ItemDataExpendable[] expendables;
}