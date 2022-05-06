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

public class InventorySystem : MonoBehaviour
{
    // Used to make slots specific to what item it can accept
    [System.Serializable]
    class slotInfo
    {
        public Item Item; // Most likely wont be needed
        public int slotId; // slot Id without subslots
        public Item.ItemTypeEnum ItemType;
        public Item.SlotTypeEnum SlotType;
        public slotInfo(Item.ItemTypeEnum inputItemType, Item.SlotTypeEnum inputSlotType, float slotId)
        {
            ItemType = inputItemType;
            SlotType = inputSlotType;
        }
    }
    [SerializeField]
    private GameObject slotPrefab;
    public List<Inventory> inventories;
    public List<GameObject> slotsUiObject;
    [SerializeField]
    private List<slotInfo> slotInfoArray = new List<slotInfo>(); // Used to check compatibility with items that are being stored
    public int selectedSlot;
    public int selectedSlot_special;

    private GameObject currentGameObject = null; // Used to check stored current GameObject to destroy it later when not used or switching item

    [System.Serializable]
    public class Items
    {
        public float slotId;
        public Item item;
    }


    [System.Serializable]
    public class Inventory
    {
        [Tooltip("set to -1 to have it as a common inventory")]
        public int specialSlotNumber;
        public List<Items> items;
    }

    // TMP
    public Item tmp_Item; // Tmp



    public class GetSlotData
    {
        public int index;

        // GetIndex_[Name] will return specific data from specified index slot
        public static string ItemName(float slotId, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].slotId == slotId)
                    {
                        return slotSystem.inventories[inventory].items[slot].item.ItemName;
                    }
                }

            }

            return null;
        }

        public static uint ItemId(float slotId, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].slotId == slotId)
                    {
                        return slotSystem.inventories[inventory].items[slot].item.ItemID;
                    }
                }

            }
            return 0; // 0 will basically mean none
        }

        public static Item.ItemTypeEnum ItemType(float slotId, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].slotId == slotId)
                    {
                        return slotSystem.inventories[inventory].items[slot].item.ItemType;
                    }
                }

            }
            return Item.ItemTypeEnum.None;
        }

        public static Item.SlotTypeEnum SlotType(float slotId, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].slotId == slotId)
                    {
                        return slotSystem.inventories[inventory].items[slot].item.SlotType;
                    }
                }

            }

            return Item.SlotTypeEnum.None;
        }

        public static Object ItemObject(float slotId, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].slotId == slotId)
                    {
                        return slotSystem.inventories[inventory].items[slot].item.ItemObject;
                    }
                }

            }
            return null;
        }

        public static Item ItemInSlot(float slotId, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].slotId == slotId)
                    {
                        return slotSystem.inventories[inventory].items[slot].item;
                    }
                }

            }
            return null;
        }
    }

    public class InventoryUtilities
    {
        // SetIndex_[Name] will set specific data to specified index slot
        public static bool SetSlotItem(float slotId, Item itemData, InventorySystem slotSystem)
        {
            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.slotInfoArray[slot].GetType() == typeof(slotInfo))
                    {
                        slotInfo slotData = slotSystem.slotInfoArray[slot];
                        if (slotData.ItemType == itemData.ItemType && slotData.SlotType == itemData.SlotType && slotData.slotId == slotId)
                        {
                            Item oldItem = slotSystem.inventories[inventory].items[slot].item;
                            Item newItem = itemData;
                            slotSystem.tmp_Item = oldItem; // [Insert] drop / remove function here
                            slotSystem.inventories[inventory].items[slot].item = newItem;
                            return true;
                        }
                    }
                }

            }


            return false;
        }


        // set_[Name] will check all indexes and if free, then place
        public static bool Set_Slot(Item itemData, InventorySystem slotSystem)
        {

            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    slotInfo slotData = slotSystem.slotInfoArray[inventory];
                    if (slotData.ItemType == itemData.ItemType && slotData.SlotType == itemData.SlotType)
                    {
                        slotSystem.inventories[inventory].items[slot].item = itemData;
                        return true;
                    }
                }

            }
            return false;
        }

        // removeIndex_[Name] will remove specific index from slot
        public static bool RemoveIndex_Item(float slotId, InventorySystem slotSystem)
        {

            for (int inventory = 0; inventory < slotSystem.inventories.Count; inventory++)
            {

                for (int slot = 0; slot < slotSystem.inventories[inventory].items.Count; slot++)
                {
                    if (slotSystem.inventories[inventory].items[slot].item != null)
                    {
                        slotSystem.inventories[inventory].items[slot].item = null;
                        return true;
                    }
                }

            }
            return false;
        }

        // remove_[Name] will remove all items from slot
        public static bool Remove_Slot(InventorySystem slotSystem)
        {
            try
            {
                for (int i = 0; i < slotSystem.inventories.Count; i++)
                {
                    slotSystem.inventories[i] = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }




    // updateIndex_[Name] will update specified index text (might come more soon)
    void UpdateIndex_Slotsobject(float slotId)
    {
        try
        {
            //slots_object[index].transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = GetSlotData.ItemName(index, this);
            //slots_object[index].transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Other";
        }
        catch
        {
            Debug.LogError("Couldn't update! Possible problem to this are 'index', 'GetChild' or 'GetComponent'");
        }
    }

    // update_[Name] will update all index text (might come more soon)
    public void Update_Slotsobject()
    {
        if (slotsUiObject == null)
        {
            foreach (GameObject slot in slotsUiObject)
            {
                Destroy(slot);
            }
        }


        try
        {
            for (int inventory = 0; inventory < inventories.Count; inventory++)
            {
                var currentInventory = inventories[inventory];

                switch (currentInventory.specialSlotNumber)
                {
                    default:
                        for (int item = 0; item < inventories[inventory].items.Count; item++)
                        {
                            InventorySlot slotData;
                            GameObject slot;

                            var currentItem = inventories[inventory].items[item];

                            slot = Instantiate(slotPrefab);

                            slotData = slot.GetComponent<InventorySlot>();

                            slot.transform.SetParent(gameObject.transform, false);

                            slotData.slotId = currentItem.slotId;
                            slotData.itemName = currentItem.item.ItemName;

                            slotsUiObject.Add(slot);

                            switch (currentItem.slotId)
                            {
                                case 1:
                                    slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Melee, 1));
                                    break;
                                case 2:
                                    slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Secondary, 2));
                                    break;
                                case 3:
                                    slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Primary, 3));
                                    break;
                                case 4:
                                    slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Grenede, 4));
                                    break;
                                case 5:
                                    slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Pickprop, 5));
                                    break;
                                case 6:
                                    slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Empty, 6));
                                    break;

                            }
                        }
                        break;

                    case 7:
                        for (int item = 0; item < inventories[inventory].items.Count; item++)
                        {
                            InventorySlot slotData;
                            GameObject slot;

                            var currentItem = inventories[inventory].items[item];

                            slot = Instantiate(slotPrefab);

                            slot.transform.SetParent(gameObject.transform, false);

                            slotData = slot.GetComponent<InventorySlot>();

                            slotData.slotId = currentItem.slotId;
                            slotData.itemName = currentItem.item.ItemName;

                            slotsUiObject.Add(slot);

                            for (int i = 0; i < inventories[inventory].items.Count; i++)
                            {
                                slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Melee, currentItem.slotId));
                            }

                        }
                        break;

                    case 8:
                        for (int item = 0; item < inventories[inventory].items.Count; item++)
                        {
                            InventorySlot slotData;
                            GameObject slot;

                            var currentItem = inventories[inventory].items[item];

                            slot = Instantiate(slotPrefab);

                            slot.transform.SetParent(gameObject.transform, false);

                            slotData = slot.GetComponent<InventorySlot>();

                            slotData.slotId = currentItem.slotId;
                            slotData.itemName = currentItem.item.ItemName;

                            slotsUiObject.Add(slot);

                            for (int i = 0; i < inventories[inventory].items.Count; i++)
                            {
                                slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Melee, currentItem.slotId));
                            }

                        }
                        break;

                    case 9:
                        for (int item = 0; item < inventories[inventory].items.Count; item++)
                        {
                            InventorySlot slotData;
                            GameObject slot;

                            var currentItem = inventories[inventory].items[item];

                            slot = Instantiate(slotPrefab);

                            slot.transform.SetParent(gameObject.transform, false);

                            slotData = slot.GetComponent<InventorySlot>();

                            slotData.slotId = currentItem.slotId;
                            slotData.itemName = currentItem.item.ItemName;

                            slotsUiObject.Add(slot);

                            for (int i = 0; i < inventories[inventory].items.Count; i++)
                            {
                                slotInfoArray.Add(new slotInfo(Item.ItemTypeEnum.BasicItem, Item.SlotTypeEnum.Melee, currentItem.slotId));
                            }

                        }
                        break;
                }


            }
        }
        catch
        {
            Debug.LogError("Couldn't update! Possible problem INSIDE for loop are 'index', 'GetChild' or 'GetComponent'");
        }
    }

    // Spawn_[Name] will spawn object to the game world to a specific parentObject
    public bool SpawnIndex_Object(float slotId)
    {


        try
        {
            bool result = false;

            if (GetSlotData.ItemObject(slotId, this) != null)
            {
                for (int inventory = 0; inventory < inventories.Count; inventory++)
                {

                    for (int slot = 0; slot < inventories[inventory].items.Count; slot++)
                    {
                        if (inventories[inventory].items[slot].slotId == slotId)
                        {
                            Debug.Log("Found slot id of " + slotId + " in inventory " + inventory + " on slot " + slot);

                            currentGameObject = Instantiate(GetSlotData.ItemObject(slotId, this)) as GameObject; // Pooling System in unity is way too confusing.. Ill research about it more later.
                            currentGameObject.transform.SetParent(transform, false);
                        }
                    }

                }

                result = true;
            }

            return result;
        }
        catch
        {
            Debug.LogError("Prefab could not be instantiated");
            return false;
        }
    }

    // removeCurrent_[Name] will remove current object if spawned any
    public bool RemoveCurrent_Object()
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

        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumMelee, 1));          // 1
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumSecondary, 2));      // 2
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumPrimary, 3));        // 3
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumGrenede, 4));        // 4
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumPickprop, 5));       // 5
        slotInfoArray.Add(new slotInfo(EnumBasicItem, EnumEmpty, 6));          // 6
        slotInfoArray.Add(new slotInfo(EnumSpecialItem, EnumSpecial, 7));      // 7
        slotInfoArray.Add(new slotInfo(EnumSpecialItem, EnumSpecial, 8));      // 7
        slotInfoArray.Add(new slotInfo(EnumSpecialItem, EnumSpecial, 9));      // 7
    }

    // Debug slot
    void Debug_Slot()
    {
        for (int iinventory = 0; iinventory < inventories.Count; iinventory++)
        {
            if (inventories[iinventory] != null)
            {
                Debug.Log("Slot_" + iinventory + " = " + inventories[iinventory].items.Count);
            }
            else
            {
                Debug.Log("Slot_" + iinventory + " = Empty");
            }
        }

    }

    // Use this as refrence if you dont know how to use my functions
    void Debug_Functions(int Type)
    {
        Item OLDITEM = tmp_Item;
        if (Type == 0) // Using Set_[Name]
        {
            if (InventoryUtilities.Set_Slot(tmp_Item, this))
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
            if (InventoryUtilities.SetSlotItem(selectedSlot, tmp_Item, this))
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
        //setupSlot(); // Must setup before using any other functions
        //InventoryUtilities.Set_Slot(tmp_Item, this);
        Update_Slotsobject();

        SpawnIndex_Object(0);
        //RemoveCurrent_Object();

        Debug.Log(GetSlotData.ItemName(7.1f, this));

    }
}