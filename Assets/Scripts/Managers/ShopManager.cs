using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    public Text ItemDescription;
    private ShopInventory _inventory;
    
    private void Start() {
        _inventory = this.GetComponent<ShopInventory>();
        
        var timeManager = TimeManager.Instance;
        if (timeManager != null) {
            timeManager.OnDayChange += () => this._updatePrice();
        }
    }

    private void _updatePrice() {
        foreach (var item in _inventory.Items) {
            item.UpdatePrice();
        }
    }

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
            ItemDescription.text = String.Format("{0}：\n价格：{1} 分\n{2}", item.Name, item.CurrentPrice, item.Description);
    }

    public void Close() {
        this.transform.parent.gameObject.SetActive(false);
    }
}
