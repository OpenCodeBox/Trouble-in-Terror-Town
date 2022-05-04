using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

// !! ========= NOTE ========= !!

// GetIndex_Id will return 0 as empty!
// So make sure you dont make any items with id 0 or below!

// !! ========= NOTE ========= !!

/*/
 * Functions available here:
 * -> GetIndex_Name
 * -> GetIndex_Id
 * -> GetIndex_ItemType
 * -> GetIndex_SlotType
 * -> GetIndex_ItemObject
 * -> GetIndex_Slot
 * 
 * -> SetIndex_Slot
 * -> Set_Slot
 * -> RemoveIndex_Slot
 * -> RemoveIndex_Slotspecial
 * -> Remove_Slot
 * -> Remove_Slotspecial
 * 
 * -> UpdateIndex_Slotsobject
 * -> Update_Slotsobject
 * 
 * -> Spawn_Object
 * -> RemoveCurrent_Object
 * 
 * -> setupSlot
 * 
 * -> Debug_Slot
 * -> Debug_Functions
/*/

public class SlotSystem : MonoBehaviour
{
    // Used to make slots specific to what item it can accept
    class slotInfo
    {
        public Item Item; // Most likely wont be needed
        public Item.ItemTypeEnum ItemType;
        public Item.SlotTypeEnum SlotType;
        public slotInfo(Item.ItemTypeEnum inputItemType, Item.SlotTypeEnum inputSlotType)
        {
            ItemType = inputItemType;
            SlotType = inputSlotType;
        }
    }

    [Header("Pooling Setting")]

    [Space(10)]
    [Header("Spawn Setting")]
    public GameObject parentObject; // Used to spawn object to specific parent
    [Space(10)]
    [Header("Slot Setting")]
    public List<Item> slots;
    public List<Item> slots_special;
    public List<GameObject> slots_object;
    private List<slotInfo> slotInfoArray = new List<slotInfo>(); // Used to check compatibility with items that are being stored
    public int selectedSlot;
    public int selectedSlot_special;

    private GameObject currentGameObject = null; // Used to check stored current GameObject to destroy it later when not used or switching item

    // TMP
    public Item tmp_Item; // Tmp

    // GetIndex_[Name] will return specific data from specified index slot
    string GetIndex_Name(int index)
    {
        if (index <= 5)
        {
            if (slots[index] != null)
            {
                return slots[index].ItemName;
            }
        }
        else if (selectedSlot_special < slots_special.Count)
        {
            if (slots_special[selectedSlot_special] != null && index == 6)
            {
                return slots_special[selectedSlot_special].ItemName;
            }
        }
        return null;
    }

    uint GetIndex_Id(int index)
    {
        if (index <= 5)
        {
            if (slots[index] != null)
            {
                return slots[index].ItemID;
            }
        }
        else if (selectedSlot_special < slots_special.Count)
        {
            if (slots_special[selectedSlot_special] != null && index == 6)
            {
                return slots_special[selectedSlot_special].ItemID;
            }
        }
        return 0; // 0 will basically mean none
    }

    Item.ItemTypeEnum GetIndex_ItemType(int index)
    {
        if (index <= 5)
        {
            if (slots[index] != null)
            {
                return slots[index].ItemType;
            }
        }
        else if (selectedSlot_special < slots_special.Count)
        {
            if (slots_special[selectedSlot_special] != null && index == 6)
            {
                return slots_special[selectedSlot_special].ItemType;
            }
        }
        return Item.ItemTypeEnum.None;
    }

    Item.SlotTypeEnum GetIndex_SlotType(int index)
    {
        if (index <= 5)
        {
            if (slots[index] != null)
            {
                return slots[index].SlotType;
            }
        }
        else if (selectedSlot_special < slots_special.Count)
        {
            if (slots_special[selectedSlot_special] != null && index == 6)
            {
                return slots_special[selectedSlot_special].SlotType;
            }
        }
        return Item.SlotTypeEnum.None;
    }

    Object GetIndex_ItemObject(int index)
    {
        if (index <= 5)
        {
            if (slots[index] != null)
            {
                return slots[index].ItemObject;
            }
        }
        else if (slots_special[selectedSlot_special] != null && index == 6)
        {
            return slots_special[selectedSlot_special].ItemObject;
        }
        return null;
    }

    Item GetIndex_Slot(int index)
    {
        if (index <= 5)
        {
            if (slots[index] != null)
            {
                return slots[index];
            }
        }
        else if (slots_special[selectedSlot_special] != null && index == 6)
        {
            return slots_special[selectedSlot_special];
        }
        return null;
    }

    // SetIndex_[Name] will set specific data to specified index slot
    bool SetIndex_Slot(int index, Item itemData)
    {
        if (slotInfoArray[index].GetType() == typeof(slotInfo))
        {
            slotInfo slotData = slotInfoArray[index];
            if (slotData.ItemType == itemData.ItemType && slotData.SlotType == itemData.SlotType)
            {
                Item oldItem = slots[index];
                Item newItem = itemData;
                tmp_Item = oldItem; // [Insert] drop / remove function here
                slots[index] = newItem;
                return true;
            }
        }
        return false;
    }

    // set_[Name] will check all indexes and if free, then place
    bool Set_Slot(Item itemData)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null && slotInfoArray[i] != null)
            {
                slotInfo slotData = slotInfoArray[i];
                if (slotData.ItemType == itemData.ItemType && slotData.SlotType == itemData.SlotType)
                {
                    slots[i] = itemData;
                    return true;
                }
            }
        }
        if (slotInfoArray[6].ItemType == itemData.ItemType && slotInfoArray[6].SlotType == itemData.SlotType)
        {
            for (int i = 0; i < slots_special.Count; i++)
            {
                if (slots_special[i].ItemID == itemData.ItemID)
                {
                    return false;
                }
            }
            slots_special.Add(itemData);
            return true;
        }
        return false;
    }

    // removeIndex_[Name] will remove specific index from slot
    bool RemoveIndex_Slot(int index)
    {
        if (slots[index] != null)
        {
            slots[index] = null;
            return true;
        }
        return false;
    }

    bool RemoveIndex_Slotspecial(int index)
    {
        try
        {
            slots_special.RemoveAt(index);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // remove_[Name] will remove all items from slot
    bool Remove_Slot()
    {
        try
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i] = null;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    bool Remove_Slotspecial()
    {
        try
        {
            slots_special.RemoveRange(0, slots_special.Count);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // updateIndex_[Name] will update specified index text (might come more soon)
    void UpdateIndex_Slotsobject(int index)
    {
        try
        {
            slots_object[index].transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = GetIndex_Name(index);
            slots_object[index].transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Other";
        }
        catch
        {
            Debug.LogError("Couldn't update! Possible problem to this are 'index', 'GetChild' or 'GetComponent'");
        }
    }

    // update_[Name] will update all index text (might come more soon)
    void Update_Slotsobject()
    {
        try
        {
            for (int i = 0; i < slots_object.Count; i++)
            {
                slots_object[i].transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = GetIndex_Name(i);
                slots_object[i].transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Other";
            }
        } catch
        {
            Debug.LogError("Couldn't update! Possible problem INSIDE for loop are 'index', 'GetChild' or 'GetComponent'");
        }
    }

    // Spawn_[Name] will spawn object to the game world to a specific parentObject
    bool SpawnIndex_Object(int index)
    {
        try
        {
            if (GetIndex_ItemObject(index) != null)
            {
                currentGameObject = Instantiate(GetIndex_ItemObject(index)) as GameObject; // Pooling System on unity is way too confusing.. Ill research about it more later.
                currentGameObject.transform.SetParent(parentObject.transform, false);
                return true;
            }
            return false;
        }
        catch
        {
            Debug.LogError("Prefab could not be instantiated");
            return false;
        }
    }

    // removeCurrent_[Name] will remove current object if spawned any
    bool RemoveCurrent_Object()
    {
        try
        {
            if (currentGameObject != null)
            {
                Destroy(currentGameObject);
                return true;
            }
            return false;
        }
        catch
        {
            Debug.LogError("currentGameObject could not be destroyed");
            return false;
        }
    }

    // Other
    // Sets up slot types for different slot placement
    void setupSlot()
    {
        Item.ItemTypeEnum EnumBasicItem = Item.ItemTypeEnum.BasicItem;
        Item.ItemTypeEnum EnumSpecialItem = Item.ItemTypeEnum.SpecialItem;

        Item.SlotTypeEnum EnumMelee = Item.SlotTypeEnum.Melee;
        Item.SlotTypeEnum EnumSecondary = Item.SlotTypeEnum.Secondary;
        Item.SlotTypeEnum EnumPrimary = Item.SlotTypeEnum.Primary;
        Item.SlotTypeEnum EnumGrenede = Item.SlotTypeEnum.Grenede;
        Item.SlotTypeEnum EnumPickprop = Item.SlotTypeEnum.Pickprop;
        Item.SlotTypeEnum EnumEmpty = Item.SlotTypeEnum.Empty;
        Item.SlotTypeEnum EnumSpecial = Item.SlotTypeEnum.Special;

        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumMelee));          // 1
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumSecondary));      // 2
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumPrimary));        // 3
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumGrenede));        // 4
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumPickprop));       // 5
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumEmpty));          // 6
        slotInfoArray.Add(new slotInfo(EnumSpecialItem, EnumSpecial));      // 7
    }

    // Debug slot
    void Debug_Slot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null)
            {
                Debug.Log("Slot_" + i + " = " + slots[i].ItemName);
            }
            else
            {
                Debug.Log("Slot_" + i + " = Empty");
            }
        }
        for (int i = 0; i < slots_special.Count; i++)
        {
            if (slots_special[i] != null)
            {
                Debug.Log("Slot_" + i + " = " + slots_special[i].ItemName);
            }
            else
            {
                Debug.Log("Slot_" + i + " = Empty");
            }
        }
    }

    // Use this as refrence if you dont know how to use my functions
    void Debug_Functions(int Type)
    {
        Item OLDITEM = tmp_Item;
        if (Type == 0) // Using Set_[Name]
        {
            if (Set_Slot(tmp_Item))
            {
                Debug.Log(OLDITEM.ItemName + " Has been stored!");
                tmp_Item = null;
            }
            else
            {
                Debug.Log(OLDITEM.ItemName + " Couldn't find free slot");
            }
        }
        else if (Type == 1) // Using SetIndex_[Name]
        {
            if (SetIndex_Slot(selectedSlot, tmp_Item))
            {
                Debug.Log(OLDITEM.ItemName + " Has been stored and dropped old item from slot!");
            }
            else
            {
                Debug.Log(OLDITEM.ItemName + " Does not fit for this slot, try another one!");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        setupSlot(); // Must setup before using any other functions
        Set_Slot(tmp_Item);
        Update_Slotsobject();

        SpawnIndex_Object(0);
        //RemoveCurrent_Object();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}