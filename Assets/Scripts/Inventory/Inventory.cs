using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("物品栏宽度"), Range(1, 16)]
    public int Width = 9;
    [Tooltip("物品栏高度"), Range(1, 8)]
    public int Height = 3;
    public void Start() {
        // 为物品栏创建空物品占位符
        for (int i = 0; i < Width * Height; ++i) {
            GameObject placeHolder = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Slot"));
            placeHolder.transform.SetParent(this.transform);
        }
    }
}