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
    Skill,
}


public class SlotHolder : MonoBehaviour, IPointerClickHandler
{
    public SlotType slotType;
    public ItemUI itemUI;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount % 2 == 0)
        { 
            UseItem();
        }
    }

    public void UseItem()
    {
        if (itemUI.GetItem() != null)
        {
            if (itemUI.GetItem().itemType == ItemType.Usable && itemUI.Bag.items[itemUI.Index].amount > 0)
            {
                // TODO: Use item
                GameManager.Instance.playerState.Heal(itemUI.GetItem().usableData.healthPoint);
                GameManager.Instance.playerState.characterData.UpdateExp(itemUI.GetItem().usableData.experiencePoint);

                itemUI.Bag.items[itemUI.Index].amount -= 1;
            }
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
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                break;
            case SlotType.Armor:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                break;
            case SlotType.Action:
                itemUI.Bag = InventoryManager.Instance.actionData;
                break;
        }

        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);
    }
}
