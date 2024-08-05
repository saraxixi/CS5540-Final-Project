using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType
{
    Bag,
    Weapon,
    Armor,
    Action,
}


public class SlotHolder : MonoBehaviour, IPointerClickHandler
{
    public Player_Health playerHealth;
    public SlotType slotType;
    public ItemUI itemUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount % 2 == 0)
        { 
            UseItem();
        }
    }

    public void UseItem()
    {
        if (itemUI.GetItem().itemType == ItemType.Usable && itemUI.Bag.items[itemUI.Index].amount > 0)
        { 
            //TODO: Use item
            playerHealth = GameObject.FindObjectOfType<Player_Health>();
            playerHealth.Heal(itemUI.GetItem().usableData.healthPoint);

            Player_Level playerLevel = GameObject.FindObjectOfType<Player_Level>();
            playerLevel.AddExperience(itemUI.GetItem().usableData.experiencePoint);

            itemUI.Bag.items[itemUI.Index].amount -= 1;
        }

        UpdateItem();
    }

    public void UpdateItem()
    {
        switch (slotType)
        { 
            case SlotType.Bag:
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            case SlotType.Weapon:
                break;
            case SlotType.Armor:
                break;
            case SlotType.Action:
                break;
        }

        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);
    }
}
