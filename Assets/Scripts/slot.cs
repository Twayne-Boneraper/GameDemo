using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public int slotID;
    public item slotItem;
    public Image image;
    public Text num;
    public GameObject itemInSlot;
    public string slotInfo;

    public void itemOnClick()
    {
        inventoryManager.instance.updateInfo(slotInfo);
    }

    public void setupSlot(item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        
        image.sprite = item.itemImage;
        num.text = item.itemNum.ToString();
        slotInfo = item.itemInfo;
    }

}
