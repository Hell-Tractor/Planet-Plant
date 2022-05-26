using UnityEngine;

/// <summary>
/// Item which can used for emotion recovery.
/// </summary>
public class EmotionRecoveryItem : Item, IUsable {

    /// <summary>
    /// Influence of this item when used
    /// </summary>
    [Header("Emotion")]
    public int EmotionValueDelta = 0;
    
    public void Use(GameObject target) {
        // influence player emotion
        if (EmotionManager.Instance != null)
            EmotionManager.Instance.EmotionValue += EmotionValueDelta;
    }

    protected new void Start() {
        base.Start();
        
        // mark this item as consumable
        this.Type = ItemType.Consumable;
    }
}
