using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for managing selections.
/// </summary>
public class SelectionManager : MonoBehaviour {
    public GameObject SelectionPrefab;

    public float ButtonHeight = 100;

    public static SelectionManager Instance;
    private GameObject[] _selection;
    private float _screenWidth = Screen.width;
    private ISelectionHandler _handler = null;
    private int _selectionCount;

    private void Awake() {
        Instance = this;
    }

    private void OnEnable() {
        // calculate button positions and size with given selection count
        _selection = new GameObject[_selectionCount];

        float top = Screen.height * 0.3f;
        float bottom = -Screen.height * 0.2f;

        float sumHeight = top - bottom;
        float width = _screenWidth / 3.0f;
        
        float height = ButtonHeight;
        float paddingHeight = (sumHeight - ButtonHeight * _selectionCount) / (_selectionCount - 1);
        
        // setting the position and size for each button
        for (int i = 0; i < _selectionCount; i++) {
            _selection[i] = Instantiate(SelectionPrefab, transform);

            float y = top - i * height;
            if (i > 0)
                y -= i * paddingHeight;
            RectTransform rectTransform = _selection[i].GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, y);
            rectTransform.sizeDelta = new Vector2(width, height);

            Button button = _selection[i].GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => this.Handle(index));
        }

        // special case for single selection
        if (_selectionCount == 1) {
            _selection[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }

    private void OnDisable() {
        // destory all buttons
        foreach (var i in _selection)
            Destroy(i);
        _selection = null;
    }

    /// <summary>
    /// Show the selection dialog.
    /// </summary>
    /// <param name="text">text shown on each button</param>
    public void ShowSelection(List<string> text) {
        _selectionCount = text.Count;
        this.gameObject.SetActive(true);
        for (int i = 0; i < _selectionCount; i++) {
            _selection[i].GetComponentInChildren<Text>().text = text[i];
        }
    }

    /// <summary>
    /// Set the handler for the selection.
    /// </summary>
    /// <param name="handler">Selection Handler</param>
    public void SetHandler(ISelectionHandler handler) {
        _handler = handler;
    }

    /// <summary>
    /// Called when a button is clicked.
    /// </summary>
    /// <param name="index">the index of the clicked button, 0 to count - 1</param>
    public void Handle(int index) {
        if (_handler != null) {
            _handler.HandleSelection(index, DialogManager.Instance);
            _handler = null;
        }
        this.gameObject.SetActive(false);
    }
}
