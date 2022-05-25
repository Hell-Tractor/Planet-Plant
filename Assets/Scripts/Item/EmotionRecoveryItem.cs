using UnityEngine;

public class EmotionRecoveryItem : Item, IUsable {

    [Header("Emotion")]
    public int EmotionValueDelta = 0;
    
    public void Use(GameObject target) {
        if (EmotionManager.Instance != null)
            EmotionManager.Instance.EmotionValue += EmotionValueDelta;
    }

    private void Start() {
        this.Type = ItemType.Consumable;
    }
}
