using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData_SO itemData; // Reference to the item data
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: Add the item to the player's inventory
            InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);
            InventoryManager.Instance.inventoryUI.RefreshUI();


            Destroy(gameObject);
        }
    }
}
