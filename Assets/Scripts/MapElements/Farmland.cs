using UnityEngine;

public class Farmland : MonoBehaviour {
    public bool HasCrop { get; private set; } = false;

    private Crop _crop;

    public bool Plant(Crop crop) {
        if (HasCrop)
            return false;
        HasCrop = true;
        _crop = crop;
        crop.transform.parent = transform;
        crop.transform.localPosition = Vector3.zero;
        return true;
    }
}
