using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToolbarManager : MonoBehaviour {
    public GameObject QuickSlot = null;
    public GameObject Map = null;
    public Text MessageBar = null;
    public Text CoinCount = null;
    
    [Header("UI显示设置")]
    public Canvas Canvas = null;
    public List<string> ShowToolBarSceneList = new List<string>();
    
    private void Start() {
        if (QuickSlot != null) {
            QuickSlot.SetActive(false);
        }
        if (Map != null) {
            Map.SetActive(false);
        }

        SceneManager.activeSceneChanged += (Scene oldScene, Scene newScene) => {
            Canvas?.gameObject.SetActive(ShowToolBarSceneList.Contains(newScene.name));
        };
    }

    private void OnGUI() {
        AI.FSM.CharacterFSM fsm = AI.FSM.CharacterFSM.Instance;
        if (fsm != null) {
            CoinCount.text = string.Format("{0:N2}元", fsm.Asset / 100.0f);
        } else {
            CoinCount.text = "N/A";
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
        this.HideMap();
        
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
