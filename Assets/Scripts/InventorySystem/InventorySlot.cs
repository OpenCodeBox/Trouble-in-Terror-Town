using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public float slotId;
    public string itemName;
    public int ammoInMag;
    public int inStorageAmmo;
    
    [SerializeField]
    private TMP_Text slotIdDisplay, itemNameDisplay, ammoCountDisplay;

    // Start is called before the first frame update
    void Start()
    {
        slotIdDisplay.text = slotId.ToString();
        itemNameDisplay.text = itemName;
    }

    public void UpdateGunAmmo()
    {
        ammoCountDisplay.text = ammoInMag + " + " + inStorageAmmo;
    }
}
