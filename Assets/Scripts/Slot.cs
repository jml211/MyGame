using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerClickHandler
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;

    public void ItemOnclicked() {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }

  

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("SDFSD");
        ItemOnclicked();
    }
}
