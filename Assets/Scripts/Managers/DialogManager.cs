using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

class AdaptedDialog {
    public string SpeakerName { get; set; }
    public string Content { get; set; }
    public Sprite SpeakerImage { get; set; }
}

class DialogAdapter {
    private int _partID;
    private List<Data.DialogData> _dialogs = new List<Data.DialogData>();
    private List<Data.CharacterData> _characters = new List<Data.CharacterData>();
    private int _counter;
    public DialogAdapter(int partid, List<Data.DialogData> dialogs, List<Data.CharacterData> characters) {
        this._counter = 0;
        this._partID = partid;

        this._dialogs = (
            from dialog in dialogs
            where dialog.PartID == partid
            select dialog
        ).ToList();

        this._characters = characters;
    }

    public AdaptedDialog GetNext() {
        if (this._counter >= this._dialogs.Count) {
            return null;
        }

        AdaptedDialog result = new AdaptedDialog();
        var dialog = this._dialogs[this._counter];
        result.SpeakerName = this._characters.Find(x => x.ID == dialog.SpeakerID).Name;
        result.Content = dialog.Content;
        string imgPath = "Drawings/" + result.SpeakerName + "/" + dialog.Emotion + ".png";
        result.SpeakerImage = Resources.Load<Sprite>(imgPath);
        this._counter++;
        return result;
    }

    public bool HasNext() {
        return this._counter < this._dialogs.Count;
    }

}

public class DialogManager : MonoBehaviour {
    public Data.CharacterDataSet CharacterDataSet;
    public Data.DialogDataSet DialogDataSet;
    public Data.SelectionDataSet SelectionDataSet;

    public Canvas DialogCanvas;
    private DialogUIController _dialogUIController = null;
    private DialogAdapter _dialogAdapter = null;
    
    private void Start() {
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_dialogAdapter != null) {
                if (_dialogUIController?.PreventKeyEventProcessing == true) {
                    return;
                }
                if (_dialogAdapter.HasNext()) {
                    var dialog = _dialogAdapter.GetNext();
                    _dialogUIController.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage);
                } else {
                    _dialogUIController.Hide();
                    _dialogAdapter = null;
                    _dialogUIController = null;
                }
            }
        }
    }

    public void ShowDialog(int partID) {
        _dialogAdapter = new DialogAdapter(partID, DialogDataSet.Dialogs, CharacterDataSet.Characters);
        if (_dialogUIController == null) {
            var canvas = Instantiate(DialogCanvas);
            _dialogUIController = canvas.GetComponent<DialogUIController>();
        }
        var dialog = _dialogAdapter.GetNext();
        _dialogUIController.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage);
    }
}
