using UnityEngine;

/// <summary>
/// Class for managing farmland.
/// </summary>
public class Farmland : MonoBehaviour {
    /// <summary>
    /// If there's any crop on current farmland.
    /// </summary>
    /// <value>true if there is.</value>
    public bool HasCrop { get; private set; } = false;

    private Crop _crop;

    /// <summary>
    /// Plant the crop into current farmland.
    /// </summary>
    /// <param name="crop">the crop to be planted</param>
    /// <returns>true if operation succeeded.</returns>
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
