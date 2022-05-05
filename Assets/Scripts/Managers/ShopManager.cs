using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    public Text ItemDescription;

    private void Update() {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        }, results);
        this._showItemDescription(null);
        foreach (var i in results) {
            if (i.gameObject.CompareTag("InventorySlot")) {
                this._showItemDescription(i.gameObject.GetComponent<Slot>().GetItem());
                break;
            }
        }
    }
    private void _showItemDescription(Item item) {
        if (item == null)
            ItemDescription.text = "";
        else
            ItemDescription.text = String.Format("{0}：\n价格：{1} 分\n{2}", item.Name, item.Price, item.Description);
    }
}
