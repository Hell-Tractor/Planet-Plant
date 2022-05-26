using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class for managing toolbar UI.
/// </summary>
public class ToolbarManager : MonoBehaviour {
    public GameObject QuickSlot = null;
    public GameObject Map = null;
    // text for showing message on map
    public Text MessageBarOverMap = null;
    // text for showing current player asset
    public Text CoinCount = null;
    // text for showing message on tool bar
    public Text MessageBar = null;

    public static ToolbarManager Instance = null;
    
    [Header("UI显示设置")]
    public Canvas Canvas = null;
    // scenes with tool bar
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
        // hide quick slot and map
        if (QuickSlot != null) {
            QuickSlot.SetActive(false);
        }
        if (Map != null) {
            Map.SetActive(false);
        }

        SceneManager.activeSceneChanged += (Scene oldScene, Scene newScene) => {
            // if the scene is not in the list, hide the tool bar
            Canvas?.gameObject.SetActive(ShowToolBarSceneList.Contains(newScene.name));
        };
    }

    private void OnGUI() {
        // update asset showing
        CoinCount.text = string.Format("{0:N2}元", GlobalProperties.Instance.PlayerAsset / 100.0f);
    }

    private void Update() {
        // update message showing
        if (messageHideTimer > 0) {
            messageHideTimer -= Time.deltaTime;
            // time's up, hide the message
            if (messageHideTimer <= 0) {
                MessageBar.text = "";
            }
        }
    }

    /// <summary>
    /// Set the QuickSlot enable if it's disabled, or disable if it's enabled.
    /// </summary>
    public void ChangeQuickSlotState() {
        if (QuickSlot != null) {
            QuickSlot.SetActive(!QuickSlot.activeSelf);
        }
    }

    /// <summary>
    /// Set the map enable.
    /// </summary>
    public void ShowMap() {
        if (Map != null) {
            Map.SetActive(true);
            QuickSlot?.SetActive(false);
        }
    }

    /// <summary>
    /// Set the map disable.
    /// </summary>
    public void HideMap() {
        if (Map != null) {
            Map.SetActive(false);
        }
    }

    /// <summary>
    /// Load scene with given scene name.
    /// </summary>
    /// <param name="targetScene">The name of the scene to be loaded.</param>
    public void LoadScene(string targetScene) {
        this.HideMap();
        
        // if the scene's name is same with current scene, do nothing
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == targetScene || targetScene.StartsWith("Going") && targetScene.EndsWith(currentScene)) {
            this.ShowMessageOverMap("已经在这里了");
            return;
        }
        
        SceneManager.LoadScene(targetScene);
    }

    /// <summary>
    /// Show the message over map.(ONLY in effect when map is shown)
    /// </summary>
    /// <param name="message">message to be shown</param>
    public void ShowMessageOverMap(string message) {
        MessageBarOverMap.text = message;
    }

    float messageHideTimer = -1;
    /// <summary>
    /// Show the message on tool bar.(ONLY in effect when map is not shown)
    /// </summary>
    /// <param name="message">message to be shown</param>
    /// <param name="duration_sec">time to be hidden</param>
    public void ShowMessage(string message, float duration_sec = 1.0f) {
        MessageBar.text = message;
        messageHideTimer = duration_sec;
    }
}
