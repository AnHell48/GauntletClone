using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemName;
    public enum ItemType
    {
        key,
        gold,
        chest,
        food,
        potion        
    }
    public ItemType itemType;

    public Items(string item_Name, ItemType item_Type )
    {
        itemName = item_Name;
        itemType =  item_Type;
    }

    public string Name 
    {
        get{return itemName;}
    }

    public ItemType Type
    {
        get{return itemType;}
    }
}
