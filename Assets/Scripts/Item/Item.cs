using UnityEngine;

/// <summary>
/// 物品类，id相同的物品视为同一物品
/// </summary>
public class Item : MonoBehaviour {
    [Tooltip("物品Id，格式：planetplant:<物品英文名称>")]
    public string Id;
    [Tooltip("物品名称")]
    public string Name;
    [Tooltip("物品描述"), Multiline(3)]
    public string Description;
    [Tooltip("当前所有者的出售价格")]
    public int Price;
    [Tooltip("物品类型")]
    public ItemType Type;

    /// <summary>
    /// 判断两个物品是否相同
    /// </summary>
    /// <param name="other">要比较的物品</param>
    public bool EqualTo(Item other) {
        return Id == other.Id;
    }

    public enum ItemType {
        Consumable,
        Weapon,
        Seed,
        Tool,
        Material
    }
}
