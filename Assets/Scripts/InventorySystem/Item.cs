using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename it later if you wish to
[CreateAssetMenu(fileName = "Item", menuName = "OpenCodeBox/Player/Item")]
public class Item : ScriptableObject
{
    public enum ItemTypeEnum
    {
        BasicItem,
        SpecialItem,
        None // If needed to return null from a function
    };

    public enum SlotTypeEnum
    {
        Melee,
        Secondary,
        Primary,
        Grenede,
        Pickprop,
        Empty,
        Special,
        None // If needed to return null from a function
    };

    public string ItemName;
    public uint ItemID;
    public ItemTypeEnum ItemType = new ItemTypeEnum();
    public SlotTypeEnum SlotType = new SlotTypeEnum();
    public int UsedSpecialSlotNumber; //used to determine which special slot to use when storing in inventory (in case it's a common item then set it to -1)
    public Object ItemObject;

}
