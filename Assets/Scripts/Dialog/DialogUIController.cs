using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class DialogUIController : MonoBehaviour {
    public UnityEngine.UI.Image DrawingImage = null;
    public UnityEngine.UI.Image ContentImage = null;
    public Transform SpeakerTransform;
    public Transform ContentTransform;

    public GameObject NormalDialog;
    public Image FullScreenDialog;

    public float FullScreenShowDuration = 1f;
    public float FullScreenHideDuration = 2f;
    public bool PreventKeyEventProcessing { get; private set; } = false;
    private Canvas[] _otherCanvas;
    private bool _activeFullScreen = false;

    private void Awake() {
        _otherCanvas = FindObjectsOfType<Canvas>();
        foreach (Canvas i in _otherCanvas)
            if (!i.CompareTag("DialogCanvas"))
                i.gameObject.SetActive(false);
        this.OnGUI();
    }
    
    private void OnGUI() {
        if (ContentImage == null || DrawingImage == null)
            return;
        ContentImage.rectTransform.sizeDelta = new Vector2(Screen.width * 0.7f, ContentImage.rectTransform.sizeDelta.y);
        RectTransform contentTransfrom = ContentTransform.GetComponent<RectTransform>();
        if (contentTransfrom != null) {
            contentTransfrom.sizeDelta = new Vector2(ContentImage.rectTransform.sizeDelta.x * 0.9f, contentTransfrom.sizeDelta.y);
        }
        DrawingImage.rectTransform.position = new Vector3(
            (Screen.width - ContentImage.rectTransform.sizeDelta.x - DrawingImage.rectTransform.sizeDelta.x) / 2,
            DrawingImage.rectTransform.sizeDelta.y / 2 + 50,
            DrawingImage.rectTransform.position.z
        );
        ContentImage.rectTransform.position = new Vector3(
            DrawingImage.rectTransform.position.x + DrawingImage.rectTransform.sizeDelta.x * 0.8f,
            ContentImage.rectTransform.sizeDelta.y / 2 + 50,
            ContentImage.rectTransform.position.z
        );
        FullScreenDialog.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        Transform fullScreenTextTransform = FullScreenDialog.transform.GetChild(0);
        fullScreenTextTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    public async void SwapDialog(bool gradually) {
        if (_activeFullScreen) {
            _activeFullScreen = false;
            PreventKeyEventProcessing = true;
            var bgTask =  _tweenImageAlpha(FullScreenDialog.gameObject, gradually ? FullScreenHideDuration : 0f, 1f, 0f);
            var textTask = _tweenTextAlpha(FullScreenDialog.transform.GetChild(0).gameObject, gradually ? FullScreenHideDuration : 0f, 1f, 0f);
            await Task.WhenAll(bgTask, textTask);
            PreventKeyEventProcessing = false;
            FullScreenDialog.gameObject.SetActive(false);
            NormalDialog.gameObject.SetActive(true);
        } else {
            _activeFullScreen = true;
            NormalDialog.SetActive(false);
            FullScreenDialog.gameObject.SetActive(true);
            PreventKeyEventProcessing = true;
            var bgTask = _tweenImageAlpha(FullScreenDialog.gameObject, gradually ? FullScreenShowDuration : 0f, 0f, 1f);
            var textTask = _tweenTextAlpha(FullScreenDialog.transform.GetChild(0).gameObject, gradually ? FullScreenShowDuration : 0f, 0f, 1f);
            await Task.WhenAll(bgTask, textTask);
            PreventKeyEventProcessing = false;
        }
    }

    private static async Task _tweenImageAlpha(GameObject obj, float time, float from, float to){
        var img = obj.GetComponent<Image>();
        float elapsedTime = 0;
        float start = from;
        while (elapsedTime < time) {
            img.color = new Color(
                img.color.r,
                img.color.g,
                img.color.b,
                Mathf.Lerp(start, to, elapsedTime / time)
            );
            elapsedTime += 0.05f;
            await Task.Delay(TimeSpan.FromSeconds(0.05f));
        }
        img.color = new Color(
            img.color.r,
            img.color.g,
            img.color.b,
            to
        );
    }

    private static async Task _tweenTextAlpha(GameObject obj, float time, float from, float to) {
        var text = obj.GetComponent<Text>();
        float elapsedTime = 0;
        float start = from;
        while (elapsedTime < time) {
            text.color = new Color(
                text.color.r,
                text.color.g,
                text.color.b,
                Mathf.Lerp(start, to, elapsedTime / time)
            );
            elapsedTime += 0.05f;
            await Task.Delay(TimeSpan.FromSeconds(0.05f));
        }
        text.color = new Color(
            text.color.r,
            text.color.g,
            text.color.b,
            to
        );
    }

    public void SetDialog(string speakerName, string content, Sprite drawing) {
        if (speakerName == "旁白") {
            if (!FullScreenDialog.gameObject.activeSelf)
                this.SwapDialog(true);
            Text text = FullScreenDialog.GetComponentInChildren<Text>();
            text.text = content;
        } else {
            if (speakerName != "unknown" && FullScreenDialog.gameObject.activeSelf)
                this.SwapDialog(true);
            if (speakerName == "unknown") {
                NormalDialog.SetActive(true);
                SpeakerTransform.gameObject.SetActive(false);
                DrawingImage.gameObject.SetActive(false);
            } else {
                SpeakerTransform.gameObject.SetActive(true);
                DrawingImage.gameObject.SetActive(true);
            }
            SpeakerTransform.GetComponent<Text>().text = speakerName;
            ContentTransform.GetComponent<Text>().text = content;
            DrawingImage.sprite = drawing;
        }
    }

    public void Hide() {
        foreach (Canvas i in _otherCanvas)
            i.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
