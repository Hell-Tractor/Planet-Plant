using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 多个相同物品堆叠
/// </summary>
public class StackedItem {
    /// <summary>
    /// 物品
    /// </summary>
    public Item item { get; set; }
    /// <summary>
    /// 数量
    /// </summary>
    public int count { get; set; }

    public StackedItem() {
        item = null;
        count = 0;
    }
    public StackedItem(Item item, int count) {
        this.item = item;
        this.count = count;
    }
}

/// <summary>
/// Inventory的每个格子
/// </summary>
public class Slot : MonoBehaviour
{
    /// <summary>
    /// 存储的物品，默认为空
    /// </summary>
    public StackedItem StoredItem = null;

    private void UpdateIcon() {
        if (StoredItem != null) {
            GetComponent<Image>().sprite = StoredItem.item.GetComponent<Image>()?.sprite;
        } else {
            GetComponent<Image>().sprite = null;
        }
    }

    /// <summary>
    /// 存储物品
    /// </summary>
    /// <param name="item">要存储的物品</param>
    /// <param name="count">要存储的物品个数</param>
    /// <returns>若已存储物品，返回之前的物品，否则返回空</returns>
    public StackedItem StoreItem(Item item, int count = 1) {
        if (this.StoredItem.item.EqualTo(item)) {
            this.StoredItem.count += count;
            return null;
        } else {
            StackedItem temp = this.StoredItem;
            this.StoredItem = new StackedItem(item, count);
            this.UpdateIcon();
            return temp;
        }
    }

    /// <summary>
    /// 存储物品
    /// </summary>
    /// <param name="item">要存储的物品</param>
    /// <returns>若已存储物品，返回之前的物品，否则返回空</returns>
    public StackedItem StoreItem(StackedItem item) {
        return StoreItem(item.item, item.count);
    }

    /// <summary>
    /// 当前格子是否为空
    /// </summary>
    public bool IsEmpty() {
        return StoredItem == null;
    }

    /// <summary>
    /// 删除当前格子中的物品
    /// </summary>
    public void RemoveItem() {
        StoredItem = null;
        this.UpdateIcon();
    }
}
