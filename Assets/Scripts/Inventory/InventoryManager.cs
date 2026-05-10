using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory baseBag;

    public GameObject slotGrid;

    public Slot slotPrefab;

    public TextMeshProUGUI itemInformation;

    private void Awake()
    {
        if (instance != null) 
            Destroy(this);
            instance = this;
       
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }
    // Update is called once per frame
    void Update()
    {

    }

    public static void UpdateItemInfo(string itemDescription) {
        instance.itemInformation.text = itemDescription;
    }

    public static void CreateNewItem(Item item) {
        Slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    public static void RefreshItem(){
        Debug.Log(instance.name);
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.baseBag.itemList.Count; i++)
        {
            CreateNewItem(instance.baseBag.itemList[i]);
        }
    }
}
