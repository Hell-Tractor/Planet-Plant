using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToolbarManager : MonoBehaviour {
    public GameObject QuickSlot = null;
    public GameObject Map = null;
    public Text MessageBar = null;
    private void Start() {
        if (QuickSlot != null) {
            QuickSlot.SetActive(false);
        }
        if (Map != null) {
            Map.SetActive(false);
        }
    }

    public void ChangeQuickSlotState() {
        if (QuickSlot != null) {
            QuickSlot.SetActive(!QuickSlot.activeSelf);
        }
    }

    public void ShowMap() {
        if (Map != null) {
            Map.SetActive(true);
            QuickSlot?.SetActive(false);
        }
    }

    public void HideMap() {
        if (Map != null) {
            Map.SetActive(false);
        }
    }

    public void LoadScene(string targetScene) {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == targetScene || targetScene.StartsWith("Going") && targetScene.EndsWith(currentScene)) {
            this.ShowMessage("已经在这里了");
            return;
        }
        SceneManager.LoadScene(targetScene);
    }

    public void ShowMessage(string message) {
        MessageBar.text = message;
    }
}
