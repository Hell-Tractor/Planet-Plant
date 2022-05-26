using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for controlling bubble dialog.
/// </summary>
public class BubbleDialogController : MonoBehaviour {
    public Text Text;

    private List<Tuple<string, Func<bool>>> _dialogQueue = new List<Tuple<string, Func<bool>>>();
    private int? _currentDialog = null;
    /// <summary>
    /// Add a dialog to queue.
    /// </summary>
    /// <param name="text">dialog to be add</param>
    /// <param name="check">condition to swap into next dialog</param>
    public void AddDialog(string text, Func<bool> check) {
        _dialogQueue.Add(new Tuple<string, Func<bool>>(text, check));
    }

    /// <summary>
    /// clear all dialogs in queue.
    /// </summary>
    public void Clear() {
        _dialogQueue.Clear();
    }

    /// <summary>
    /// Show bubble dialog with delay option
    /// </summary>
    /// <param name="delay_sec">second(s) to delay</param>
    public async void Show(float delay_sec = 0) {
        // if delay is valid, delay
        if (delay_sec > 0) {
            await Task.Delay((int)(delay_sec * 1000));
        }
        
        // show bubble dialog and init
        this.gameObject.SetActive(true);
        _currentDialog = 0;
        this.Text.text = _dialogQueue[_currentDialog.Value].Item1;
    }

    private void Update() {
        if (_currentDialog != null && _currentDialog < _dialogQueue.Count) {
            // check condition every frame
            if (_dialogQueue[_currentDialog.Value].Item2()) {
                if (++_currentDialog == _dialogQueue.Count) {
                    // if all dialog is shown, hide bubble dialog
                    this.gameObject.SetActive(false);
                    _currentDialog = null;
                } else {
                    this.Text.text = _dialogQueue[_currentDialog.Value].Item1;
                }
            }
        }
    }
}
