using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new inventory",menuName ="Inventory/new Inventory")]
public class inventory : ScriptableObject
{
    public List<item> itemList = new List<item>();
}
