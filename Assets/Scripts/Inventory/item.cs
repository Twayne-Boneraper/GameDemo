using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Item",menuName ="Inventory/New Item")]
public class item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemNum;

    [TextArea]
    public string itemInfo;
    public bool equip;
}
