using System;
using UnityEngine;
public class ShopInventory : Inventory {
    private bool _isFirstTimeBuyEmotionRecItem = true;
    
    public override void Init() {
        base.Init();

        this.OnItemClick += (Slot slot) => {
            if (slot.Type != SlotType.PickOnly)
                return true;
            var item = slot.GetItem();
            if (item == null)
                return false;
            if ((AI.FSM.CharacterFSM.Instance?.Asset ?? -100) >= item.CurrentPrice) {
                AI.FSM.CharacterFSM.Instance.Asset -= item.CurrentPrice;

                if (item.Type == ItemType.Consumable && _isFirstTimeBuyEmotionRecItem) {
                    _isFirstTimeBuyEmotionRecItem = false;
                    DialogManager.Instance.ShowDialog(19);
                }
                return true;
            }
            return false;
        };
    }

    private Action _onClose = null;
    
    public void Show(Action onClose = null) {
        _onClose = onClose;
        this.transform.parent.gameObject.SetActive(true);
    }

    private void OnDisable() {
        _onClose?.Invoke();
        _onClose = null;
    }
}
