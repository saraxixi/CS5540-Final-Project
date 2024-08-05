using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Usable,
    Weapon,
    Armor
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]

public class ItemData_SO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;
    [TextArea]
    public string description = "";
    public bool stackable;

    [Header("Usable")]
    public GameObject usablePrefab;
    public UsableItemData_SO usableData;
}
