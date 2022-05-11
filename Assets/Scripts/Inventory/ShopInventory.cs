using UnityEngine;
public class ShopInventory : Inventory {
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
                return true;
            }
            return false;
        };
    }
}
