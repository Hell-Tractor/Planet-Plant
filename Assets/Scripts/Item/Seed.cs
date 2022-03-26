using UnityEngine;

public class Seed : Item, IUsable {
    public GameObject CropPrefab;

    Seed() {
        Type = ItemType.Seed;
    }
    
    public void Use(GameObject target) {
        if (target == null)
            return;
        Farmland farmland = target.GetComponent<Farmland>();
        if (farmland == null)
            return;
        if (farmland.HasCrop)
            return;
        GameObject crop = Instantiate(CropPrefab);
        farmland.Plant(crop.GetComponent<Crop>());
    }
}
