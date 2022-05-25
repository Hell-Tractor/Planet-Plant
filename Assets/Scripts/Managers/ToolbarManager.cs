using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToolbarManager : MonoBehaviour {
    public GameObject QuickSlot = null;
    public GameObject Map = null;
    public Text MessageBarOverMap = null;
    public Text CoinCount = null;
    public Text MessageBar = null;

    public static ToolbarManager Instance = null;
    
    [Header("UI显示设置")]
    public Canvas Canvas = null;
    public List<string> ShowToolBarSceneList = new List<string>();
    
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("ToolbarManager 重复实例");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

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
        CoinCount.text = string.Format("{0:N2}元", GlobalProperties.Instance.PlayerAsset / 100.0f);
    }

    private void Update() {
        if (messageHideTimer > 0) {
            messageHideTimer -= Time.deltaTime;
            if (messageHideTimer <= 0) {
                MessageBar.text = "";
            }
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
            this.ShowMessageOverMap("已经在这里了");
            return;
        }
        SceneManager.LoadScene(targetScene);
    }

    public void ShowMessageOverMap(string message) {
        MessageBarOverMap.text = message;
    }

    float messageHideTimer = -1;
    public void ShowMessage(string message, float duration_sec = 1.0f) {
        MessageBar.text = message;
        messageHideTimer = duration_sec;
    }
}
