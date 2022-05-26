using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class for managing shop UI.
/// </summary>
public class ShopManager : MonoBehaviour {
    public Text ItemDescription;
    private ShopInventory _inventory;
    
    private void Start() {
        _inventory = this.GetComponent<ShopInventory>();
        
        var timeManager = TimeManager.Instance;
        if (timeManager != null) {
            // register the price update to time manager
            timeManager.OnDayChange += () => this._updatePrice();
        }
    }

    private void _updatePrice() {
        // update all prices of item in shop
        foreach (var item in _inventory.Items) {
            item.UpdatePrice();
        }
    }

    private void Update() {
        // find the item under mouse
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
            position = Input.mousePosition
        }, results);
        // clear item description
        this._showItemDescription(null);
        // show current item description
        foreach (var i in results) {
            if (i.gameObject.CompareTag("InventorySlot")) {
                this._showItemDescription(i.gameObject.GetComponent<Slot>().GetItem());
                break;
            }
        }
    }

    /// <summary>
    /// Show the item description.
    /// </summary>
    /// <param name="item">item which is going to be shown, can be null</param>
    private void _showItemDescription(Item item) {
        // if null, show nothing
        if (item == null)
            ItemDescription.text = "";
        else
            ItemDescription.text = String.Format("{0}：\n价格：{1} 分\n{2}", item.Name, item.CurrentPrice, item.Description);
    }

    /// <summary>
    /// Close the shop
    /// </summary>
    public void Close() {
        this.transform.parent.gameObject.SetActive(false);
    }
}
