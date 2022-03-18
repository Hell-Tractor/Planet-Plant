using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Data/InventoryData", order = 1)]
public class InventoryData : ScriptableObject
{
    [System.Serializable]
    public class ItemData {
        [Tooltip("物品Id")]
        public string Id;
        [Tooltip("物品个数"), Range(1, 999)]
        public int Count;
    }

    public List<ItemData> Items = new List<ItemData>();
}
