using System;
using UnityEngine;

public class QuickSlot : Inventory {
    private int _selectedSlot = 0;

    private new void Update() {
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollWheel != 0) {
            ChangeSlot(Math.Sign(mouseScrollWheel));
        }

        if (Input.GetMouseButtonDown(1)) {
            Item item = _slots[_selectedSlot].GetItem();
            if (item != null && (item.Type == ItemType.Consumable || item.Type == ItemType.Seed || item.Type == ItemType.Tool)) {
                (item as IUsable).Use(GetTarget());
            }
        }

        // todo 高亮当前选中的格子
    }

    // todo 添加获取目标方法
    public GameObject GetTarget() {
        // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return null;
    }

    public void ChangeSlot(int delt) {
        _selectedSlot += delt;
        if (_selectedSlot < 0)
            _selectedSlot += Width;
        _selectedSlot %= Width;
    }
}