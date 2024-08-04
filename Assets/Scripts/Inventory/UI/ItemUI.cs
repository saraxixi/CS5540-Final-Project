using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image icon = null;
    public Text amountText = null;

    public void SetupItemUI(ItemData_SO item, int itemAmount)
    {
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            amountText.text = itemAmount.ToString();
            icon.gameObject.SetActive(true);
        }
        else
        { 
            icon.gameObject.SetActive(false);
        }
    }
}
