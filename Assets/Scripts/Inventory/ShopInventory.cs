using System;
using UnityEngine;
/// <summary>
/// Inventory for shop.
/// </summary>
public class ShopInventory : Inventory {
    private bool _isFirstTimeBuyEmotionRecItem = true;
    
    public override void Init() {
        base.Init();

        // check before buying item
        this.OnItemClick += (Slot slot) => {
            // not pickonly slot, skip
            if (slot.Type != SlotType.PickOnly)
                return true;
                
            var item = slot.GetItem();
            
            // no item in clicked slot, skip
            if (item == null)
                return false;
            
            // check if money is enough
            if (GlobalProperties.Instance.PlayerAsset >= item.CurrentPrice) {
                GlobalProperties.Instance.PlayerAsset -= item.CurrentPrice;

                // if item is emotion rec item and is first time buying, show hint message
                if (item.Type == ItemType.Consumable && _isFirstTimeBuyEmotionRecItem) {
                    _isFirstTimeBuyEmotionRecItem = false;
                    DialogManager.Instance.ShowDialog(19);
                }
                return true;
            }

            // money is not enough, show hint message
            ToolbarManager.Instance.ShowMessage("钱不够啦，买不起这个东西");

            return false;
        };
    }

    private Action _onClose = null;
    
    /// <summary>
    /// Show the shop, and set the on close action.
    /// </summary>
    /// <param name="onClose">action to be called when shop closed(only active once)</param>
    public void Show(Action onClose = null) {
        _onClose = onClose;
        this.transform.parent.gameObject.SetActive(true);
    }

    private void OnDisable() {
        // call on close action
        _onClose?.Invoke();
        _onClose = null;
    }
}
