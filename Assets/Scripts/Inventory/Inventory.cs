using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [Tooltip("物品栏宽度"), Range(1, 16)]
    public int Width = 9;
    [Tooltip("物品栏高度"), Range(1, 8)]
    public int Height = 3;

    public Item _itemFollowMouse = null;
    
    protected Slot[] _slots;

  public void Start() {
        _slots = new Slot[Width * Height];
        // 为物品栏创建空物品占位符
        for (int i = 0; i < Width * Height; ++i) {
            GameObject placeHolder = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Slot"));
            placeHolder.transform.SetParent(this.transform);

            _slots[i] = placeHolder.GetComponent<Slot>();
        }
    }

    public void Update() {
        // 物品跟随鼠标
        if (_itemFollowMouse != null) {
            _itemFollowMouse.transform.position = Input.mousePosition;
        }
        // 鼠标点击物品栏，交换物品
        if (Input.GetMouseButtonDown(0)) {
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) {
                position = Input.mousePosition
            }, hits);
            foreach (var i in hits) {
                if (i.gameObject.CompareTag("InventorySlot")) {
                    _itemFollowMouse = i.gameObject.GetComponent<Slot>().StoreItem(_itemFollowMouse);
                    break;
                }
            }
        }
    }
}