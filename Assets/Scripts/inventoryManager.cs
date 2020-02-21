using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryManager : MonoBehaviour
{
    public static inventoryManager instance;

    public inventory myBag;
    public GameObject slotGrid;
    //public slot slotPrefab;
    public GameObject emptySlot;
    public Text info;


    public List<GameObject> slots = new List<GameObject>();
    private void OnEnable()
    {
        refreshItem();
        //instance.info.text = "";
    }

    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void updateInfo(string info)
    {
        inventoryManager.instance.info.text = info;
    }

    //public static void creatNewItem(item item)
    //{
    //    slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //    newItem.slotItem = item;
    //    newItem.image.sprite = item.itemImage;
    //    newItem.num.text = item.itemNum.ToString();   
    //}

    public static void refreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            //creatNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<slot>().setupSlot(instance.myBag.itemList[i]);
            instance.slots[i].GetComponent<slot>().slotID = i;
        }
    }

}
