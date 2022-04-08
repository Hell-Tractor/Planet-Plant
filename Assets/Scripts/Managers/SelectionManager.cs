using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {
    [Range(1, 4)]
    public int SelectionCount = 3;
    public GameObject SelectionPrefab;

    public float ButtonHeight = 100;

    private GameObject[] _selection;
    private float _screenWidth = Screen.width;
    private ISelectionHandler _handler = null;

    private void OnEnable() {
        _selection = new GameObject[SelectionCount];

        float top = Screen.height * 0.3f;
        float bottom = -Screen.height * 0.2f;

        float sumHeight = top - bottom;
        float width = _screenWidth / 3.0f;
        
        float height = ButtonHeight;
        float paddingHeight = (sumHeight - ButtonHeight * SelectionCount) / (SelectionCount - 1);
        

        for (int i = 0; i < SelectionCount; i++) {
            _selection[i] = Instantiate(SelectionPrefab, transform);

            float y = top - i * height;
            if (i > 0)
                y -= i * paddingHeight;
            RectTransform rectTransform = _selection[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, y);
            rectTransform.sizeDelta = new Vector2(width, height);
        }

        if (SelectionCount == 1) {
            _selection[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }

    private void OnDisable() {
        foreach (var i in _selection)
            Destroy(i);
        _selection = null;
    }

    public void SetText(string[] text) {
        for (int i = 0; i < SelectionCount; i++) {
            _selection[i].GetComponentInChildren<Text>().text = text[i];
        }
    }

    public void SetHandler(ISelectionHandler handler) {
        _handler = handler;
    }

    public void Handle(int index) {
        if (_handler != null)
            _handler.HandleSelection(index, DialogManager.Instance);
    }
}
