using UnityEngine;

/// <summary>
/// Base class of all seeds.
/// </summary>
public class Seed : Item, IUsable {
    /// <summary>
    /// Prefab of the crop when seed is planted.
    /// </summary>
    public GameObject CropPrefab;

    public Seed() {
        // mark this item as seed
        Type = ItemType.Seed;
    }
    
    public void Use(GameObject target) {
        // no target planting, skip
        if (target == null)
            return;
        Farmland farmland = target.GetComponent<Farmland>();
        // not farmland, not plantable, skip
        if (farmland == null)
            return;
        // farmland is already planted, skip
        if (farmland.HasCrop)
            return;
        // plant seed
        Debug.Log("Planting seed");
        this.Count--;
        GameObject crop = Instantiate(CropPrefab);
        farmland.Plant(crop.GetComponent<Crop>());
    }
}
