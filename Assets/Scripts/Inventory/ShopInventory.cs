public class ShopInventory : Inventory {
    public override void Init() {
        base.Init();

        this.OnItemClick += (Item item) => {
            if (item == null)
                return false;
            if ((AI.FSM.CharacterFSM.Instance?.Asset ?? -100) >= item.Price) {
                AI.FSM.CharacterFSM.Instance.Asset -= item.Price;
                return true;
            }
            return false;
        };
    }
}
