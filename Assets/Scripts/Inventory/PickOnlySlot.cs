using UnityEngine;
public class PickOnlySlot : Slot {
    public override Item StoreItem(Item item) {
        if (!this._item || item != null && this._item.EqualTo(item) != true)
            return item;
        this._item.Count--;
        if (this._item.Count <= 0) {
            this.RemoveItem();
        } else {
            this._item.UpdateCount();
        }
        if (item == null) {
            Item newItem = Instantiate<GameObject>(this._item.gameObject, FindObjectOfType<Canvas>().transform).GetComponent<Item>();
            newItem.Count = 1;
            newItem.UpdateCount();
            return newItem;
        } else {
            item.Count++;
            item.UpdateCount();
            return item;
        }
    }
}