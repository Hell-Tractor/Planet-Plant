using UnityEngine;
/// <summary>
/// Slot that can only be picked up.
/// </summary>
public class PickOnlySlot : Slot {
    public override void Init() {
        this.Type = SlotType.PickOnly;
    }
    /// <summary>
    /// exchange item with item in slot
    /// </summary>
    /// <param name="item">item to be stored into slot, allow null</param>
    /// <param name="canvas">canvas of current inventory</param>
    /// <returns>exchanged item, might be null</returns>
    public override Item StoreItem(Item item, Canvas canvas) {
        // if slot is empty or comming item is not save with item in slot, do nothing
        if (!this._item || item != null && this._item.EqualTo(item) != true)
            return item;
        
        // update item count
        this._item.Count--;

        // remove item with count zero
        if (this._item.Count <= 0) {
            this.RemoveItem();
        } else {
            this._item.UpdateCount();
        }
        
        if (item == null) {
            // instantiate a new item if comming item is null
            Item newItem = Instantiate<GameObject>(this._item.gameObject, canvas.transform).GetComponent<Item>();
            newItem.Count = 1;
            newItem.UpdateCount();
            return newItem;
        } else {
            // the comming item must be same with item in slot, update the count directly
            item.Count++;
            item.UpdateCount();
            return item;
        }
    }
}