using UnityEngine;

public class Seed : Item, IUsable {
    public GameObject CropPrefab;

    Seed() {
        Type = ItemType.Seed;
    }
    
    public void Use(GameObject target) {
        Debug.Log("Use seed");
        if (target == null)
            return;
        Farmland farmland = target.GetComponent<Farmland>();
        if (farmland == null)
            return;
        if (farmland.HasCrop)
            return;
        Debug.Log("Planting seed");
        this.Count--;
        GameObject crop = Instantiate(CropPrefab);
        farmland.Plant(crop.GetComponent<Crop>());
    }
}
