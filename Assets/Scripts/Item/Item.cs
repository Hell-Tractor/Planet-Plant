using UnityEngine;
using UnityEngine.UI;

public enum ItemType {
    None,
    Consumable,
    Weapon,
    Seed,
    Tool,
    Material
}

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
    [Tooltip("当前所有者的出售单价")]
    public int InitPrice;
    [ReadOnly]
    public int CurrentPrice;
    [Tooltip("物品类型")]
    public ItemType Type;
    [Tooltip("物品个数"), Range(1, 999)]
    public int Count = 1;

    // todo 需要调整
    [Header("Update Parameters")]
    public float Mu0 = 1f;
    public float sigma0 = 0.2f;
    public float eps0 = 0.005f;
    public float Mu1 = 0f;
    public float sigma1 = 0.5f;
    public float eps1 = 0;

    protected void Start() {
        this.UpdateCount();
        this.GetComponent<RectTransform>().localPosition = Vector3.zero;
        this.CurrentPrice = this.InitPrice;
    }

    /// <summary>
    /// 判断两个物品是否相同
    /// </summary>
    /// <param name="other">要比较的物品</param>
    public bool EqualTo(Item other) {
        return other && Id == other.Id;
    }

    /// <summary>
    /// 更新数量显示
    /// </summary>
    public void UpdateCount() {
        var textComponent = this.GetComponentInChildren<Text>();
        if (textComponent)
            textComponent.text = Count.ToString();
    }

    /// <summary>
    /// Update Item Price using formula
    /// </summary>
    public virtual void UpdatePrice() {
        float lastPrice = this.CurrentPrice;
        float f = Utils.Random.Normal(Mu0 + eps0 * (InitPrice - lastPrice), sigma0) * lastPrice +
                    Utils.Random.Normal(Mu1 + eps1 * (InitPrice - lastPrice), sigma1);
        this.CurrentPrice = Mathf.RoundToInt(
            f
        );
        Debug.Log(f);
    }
}
