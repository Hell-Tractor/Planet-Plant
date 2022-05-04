using UnityEngine;

public class ToolbarManager : MonoBehaviour {
    public GameObject QuickSlot = null;
    private void Start() {
        if (QuickSlot != null) {
            QuickSlot.SetActive(false);
        }
    }

    public void ChangeQuickSlotState() {
        if (QuickSlot != null) {
            QuickSlot.SetActive(!QuickSlot.activeSelf);
        }
    }
}
