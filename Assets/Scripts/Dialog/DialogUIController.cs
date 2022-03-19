using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class DialogUIController : MonoBehaviour {
    public UnityEngine.UI.Image DrawingImage = null;
    public UnityEngine.UI.Image ContentImage = null;
    public Transform SpeakerTransform;
    public Transform ContentTransform;

    private Canvas[] _otherCanvas;

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
            DrawingImage.rectTransform.position.y,
            DrawingImage.rectTransform.position.z
        );
        ContentImage.rectTransform.position = new Vector3(
            DrawingImage.rectTransform.position.x + DrawingImage.rectTransform.sizeDelta.x,
            ContentImage.rectTransform.position.y,
            ContentImage.rectTransform.position.z
        );
    }

    public void SetDialog(string speakerName, string content, Sprite drawing) {
        SpeakerTransform.GetComponent<Text>().text = speakerName;
        ContentTransform.GetComponent<Text>().text = content;
        DrawingImage.sprite = drawing;
    }

    public void Hide() {
        foreach (Canvas i in _otherCanvas)
            i.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
