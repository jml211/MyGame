using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{

    public Item thisItem;
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
       // AddNewItem();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddNewItem() {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            Debug.Log("add" + thisItem.itemHeld);
        }
        else {
            Debug.Log("add" + thisItem.itemHeld);
            thisItem.itemHeld++;
        }
        InventoryManager.RefreshItem();
    }
}
