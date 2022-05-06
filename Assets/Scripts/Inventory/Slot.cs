using UnityEngine;

public enum SlotType {
    Normal,
    PickOnly
}

/// <summary>
/// Inventory的每个格子
/// </summary>
public class Slot : MonoBehaviour
{
    /// <summary>
    /// 存储的物品，默认为空
    /// </summary>
    protected Item _item = null;
    [ReadOnly]
    public SlotType Type;

    private void Awake() {
        this.Init();
    }

    /// <summary>
    /// 子类必须初始化Type
    /// </summary>
    public virtual void Init() {
        this.Type = SlotType.Normal;
    }

    /// <summary>
    /// 存储物品
    /// </summary>
    /// <param name="item">要存储的物品</param>
    /// <param name="inventory">物品栏</param>
    /// <returns>若存储前Slot为空返回null，否则返回Slot中Item</returns>
    public virtual Item StoreItem(Item item) {
        if (this._item?.EqualTo(item) == true) {
            this._item.Count += item.Count;
            this._item.UpdateCount();
            Destroy(item.gameObject);
            return null;
        } else {
            this._item?.transform.SetParent(FindObjectOfType<Canvas>().transform);
            var temp = this._item;
            this._item = item;
            this._item?.transform.SetParent(this.transform);
            if (this._item) {
                this._item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            return temp;
        }
    }

    /// <summary>
    /// 当前格子是否为空
    /// </summary>
    public bool IsEmpty() {
        return _item == null;
    }

    /// <summary>
    /// 删除当前格子中的物品
    /// </summary>
    public void RemoveItem() {
        if (_item != null) {
            Destroy(_item.gameObject);
            _item = null;
        }
    }

    /// <summary>
    /// 获取当前格子中物品
    /// </summary>
    /// <returns></returns>
    public Item GetItem() {
        return _item;
    }

    public void SetItem(Item item) {
        if (this._item != null)
            this.RemoveItem();
        this._item = item;
        this._item.transform.SetParent(this.transform);
    }
}
