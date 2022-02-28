using UnityEngine;

/// <summary>
/// Inventory的每个格子
/// </summary>
public class Slot : MonoBehaviour
{
    /// <summary>
    /// 存储的物品，默认为空
    /// </summary>
    protected Item _item = null;

    /// <summary>
    /// 存储物品
    /// </summary>
    /// <param name="item">要存储的物品</param>
    /// <param name="inventory">物品栏</param>
    public void StoreItem(Item item) {
        if (this._item?.EqualTo(item) == true) {
            this._item.Count += item.Count;
            this._item.UpdateCount();
            Destroy(item.gameObject);
        } else {
            this._item?.transform.SetParent(FindObjectOfType<Canvas>().transform);
            var temp = this._item;
            this._item = item;
            this._item?.transform.SetParent(this.transform);
            item = this._item;
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
}
