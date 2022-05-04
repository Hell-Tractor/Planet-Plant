using System;
using UnityEngine;

public class QuickSlot : Inventory {
    private int _selectedSlot = 0;

    private new void Update() {
        base.Update();
        
        // float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        // if (mouseScrollWheel != 0) {
        //     ChangeSlot(Math.Sign(mouseScrollWheel));
        // }

        if (Input.GetMouseButtonDown(1)) {
            // Item item = _slots[_selectedSlot].GetItem();
            Item item = Inventory._itemFollowMouse;
            if (item != null && (item.Type == ItemType.Consumable || item.Type == ItemType.Seed || item.Type == ItemType.Tool)) {
                (item as IUsable).Use(GetTarget());
                item.UpdateCount();
                if (item.Count <= 0) {
                    Destroy(Inventory._itemFollowMouse.gameObject);
                    Inventory._itemFollowMouse = null;
                }
            }
        }
        // deprecated:
        // todo 高亮当前选中的格子
    }   

    public GameObject GetTarget() {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, LayerMask.GetMask("Farmland"));
        // Debug.Log(hit.collider);
        return hit.collider?.gameObject;        
    }

    public void ChangeSlot(int delt) {
        _selectedSlot += delt;
        if (_selectedSlot < 0)
            _selectedSlot += Width;
        _selectedSlot %= Width;
    }
}