using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PermanentNode : MonoBehaviour {
    public static PermanentNode Instance;
    [Header("Brightness Mask Settings")]
    public Canvas BrightnessCanvas;
    public GameObject BrightnessMask;
    public float Duration = 1f;
    public float Interval = 0.1f;
    public float MaxAlpha = 40f / 255;

    [Header("UI显示设置")]
    public Canvas Canvas = null;
    // scenes with tool bar
    public List<string> ShowToolBarSceneList = new List<string>();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        // if scene is not in the list, hide the tool bar
        Canvas?.gameObject.SetActive(ShowToolBarSceneList.Contains(SceneManager.GetActiveScene().name));
        SceneManager.activeSceneChanged += (Scene oldScene, Scene newScene) => {
            Canvas?.gameObject.SetActive(ShowToolBarSceneList.Contains(newScene.name));
            DialogManager.Instance?.IgnoreCanvas.Add(BrightnessCanvas);
        };
    }

    /// <summary>
    /// Change the brightness mask's alpha value.
    /// </summary>
    /// <param name="enable">true: from hide to show</param>
    /// <param name="gradually">true: change gradually</param>
    public async void EnableBrightnessMask(bool enable, bool gradually = true) {
        float startValue = enable ? 0f : MaxAlpha;
        float endValue = enable ? MaxAlpha : 0f;
        if (gradually) {
            
            float timer = 0;
            while (timer < Duration) {
                Color color = BrightnessMask.GetComponent<Image>().color;
                BrightnessMask.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(startValue, endValue, timer / Duration));
                await Task.Delay(TimeSpan.FromSeconds(Interval));
                timer += Interval;
            }
        } else {
            Color color = BrightnessMask.GetComponent<Image>().color;
            BrightnessMask.GetComponent<Image>().color = new Color(color.r, color.g, color.b, endValue);
        }
    }
}
