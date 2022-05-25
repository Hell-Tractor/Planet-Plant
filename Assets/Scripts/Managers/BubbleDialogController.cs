using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleDialogController : MonoBehaviour {
    public Text Text;

    private List<Tuple<string, Func<bool>>> _dialogQueue = new List<Tuple<string, Func<bool>>>();
    private int? _currentDialog = null;
    public void AddDialog(string text, Func<bool> check) {
        _dialogQueue.Add(new Tuple<string, Func<bool>>(text, check));
    }

    public void Clear() {
        _dialogQueue.Clear();
    }

    public async void Show(float delay_sec = 0) {
        if (delay_sec > 0) {
            await Task.Delay((int)(delay_sec * 1000));
        }
        this.gameObject.SetActive(true);
        _currentDialog = 0;
        this.Text.text = _dialogQueue[_currentDialog.Value].Item1;
    }

    private void Update() {
        if (_currentDialog != null && _currentDialog < _dialogQueue.Count) {
            if (_dialogQueue[_currentDialog.Value].Item2()) {
                if (++_currentDialog == _dialogQueue.Count) {
                    this.gameObject.SetActive(false);
                    _currentDialog = null;
                } else {
                    this.Text.text = _dialogQueue[_currentDialog.Value].Item1;
                }
            }
        }
    }
}
