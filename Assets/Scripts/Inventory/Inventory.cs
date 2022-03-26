using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

/// <summary>
/// 物品栏
/// </summary>
public class Inventory : MonoBehaviour, ISaveLoad
{
    [Tooltip("物品栏宽度"), Range(1, 16)]
    public int Width = 9;
    [Tooltip("物品栏高度"), Range(1, 8)]
    public int Height = 3;

    [Tooltip("Slot的Prefab")]
    public GameObject SlotPrefab;
    [Tooltip("存储物品的InventoryData")]
    public InventoryData InventoryData;

    [HideInInspector]
    public static Item _itemFollowMouse = null;
    
    protected Slot[] _slots;

    public void Start() {
        _slots = new Slot[Width * Height];
        // 为物品栏创建空物品占位符
        for (int i = 0; i < Width * Height; ++i) {
            GameObject placeHolder = Instantiate<GameObject>(SlotPrefab);
            placeHolder.transform.SetParent(this.transform);

            _slots[i] = placeHolder.GetComponent<Slot>();
        }
        // 加载物品
        this.Load();
        // 关闭自动保存/加载
        this.DisableAutoSaveLoad();
    }

    public void OnDisable() {
        // 保存物品
        this.Save();
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
            Debug.Log(hits.Count);
            foreach (var i in hits) {
                if (i.gameObject.CompareTag("InventorySlot")) {
                    _itemFollowMouse = i.gameObject.GetComponent<Slot>().StoreItem(_itemFollowMouse);
                    break;
                }
            }
        }
    }
    
    public bool AddItem(Item item) {
        Slot slot = this._findEmptySlot();
        if (slot != null) {
            slot.StoreItem(item);
            return true;
        }
        return false;
    }

    private Slot _findEmptySlot() {
        foreach (var slot in _slots) {
            Slot temp = slot.GetComponent<Slot>();
            if (temp.IsEmpty()) {
                return temp;
            }
        }
        return null;
    }

    #region ISaveLoad
    public void Save() {
        var data = this.GetDataContainer() as InventoryData;
        if (data == null) {
            Debug.LogWarning("InventoryData is null");
            return;
        }
        data.Items.Clear();
        foreach (var i in _slots) {
            if (i.GetItem() != null) {
                data.Items.Add(new InventoryData.ItemData {
                    Id = i.GetItem().Id,
                    Count = i.GetItem().Count
                });
            } else {
                data.Items.Add(new InventoryData.ItemData {
                    Id = "",
                    Count = 0
                });
            }
        }
    }

    public void Load() {
        var data = this.GetDataContainer() as InventoryData;
        if (data == null) {
            Debug.LogWarning("InventoryData is null");
            return;
        }
        for (int i = 0; i < data.Items.Count; ++i) {
            if (i >= _slots.Length) {
                Debug.LogWarning("InventoryData中的物品数量超过了Inventory的容量，已跳过");
                break;
            }
            if (data.Items[i].Id != "") {
                _slots[i].RemoveItem();
                GameObject temp = Resources.Load<GameObject>("Prefabs/Items/" + data.Items[i].Id.Split(':')[1]);
                if (temp == null) {
                    Debug.LogWarning("未找到物品：" + data.Items[i].Id);
                    continue;
                }
                Item item = Instantiate<GameObject>(temp).GetComponent<Item>();
                item.Count = data.Items[i].Count;
                _slots[i].StoreItem(item);
            }
        }
    }

    public ScriptableObject GetDataContainer() {
        return InventoryData;
    }
    #endregion
}