using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData_SO itemData; // Reference to the item data
    private bool canPickup; // Check if the player can pick up the item
    
    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.F))
        {
            PickupItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            TutorialManager.Instance.SetText("Press F to pick up ");
        }
    }

    private void PickupItem()
    {
        InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);
        InventoryManager.Instance.inventoryUI.RefreshUI();
        QuestManager.Instance.UpdateQuestProgress(this.name, 1);
        Destroy(gameObject);
    }
}
