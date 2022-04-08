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
    public DialogAdapter(int partid, List<Data.DialogData> dialogs, List<Data.CharacterData> characters, int startID = 0) {
        this._counter = startID;
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

        return GetDialog(this._counter++);
    }

    public AdaptedDialog GetDialog(int id) {
        if (id >= this._dialogs.Count) {
            return null;
        }
        this._counter = id;
        
        AdaptedDialog result = new AdaptedDialog();
        var dialog = this._dialogs[this._counter];
        result.SpeakerName = this._characters.Find(x => x.ID == dialog.SpeakerID).Name;
        result.Content = dialog.Content;
        string imgPath = "Drawings/" + result.SpeakerName + "/" + dialog.Emotion;
        result.SpeakerImage = Resources.Load<Sprite>(imgPath);
        return result;
    }

    public bool HasNext() {
        return this._counter < this._dialogs.Count;
    }

    public void SetCounter(int count) {
        this._counter = count;
    } 
}

public class DialogManager : MonoBehaviour {
    public Data.CharacterDataSet CharacterDataSet;
    public Data.DialogDataSet DialogDataSet;
    public Data.SelectionDataSet SelectionDataSet;

    public Canvas DialogCanvas;
    public static DialogManager Instance;
    private DialogUIController _dialogUIController = null;
    private DialogAdapter _dialogAdapter = null;
    
    private void Start() {
        Instance = this;
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

    public void ShowDialog(int partID, int startID = 0) {
        _dialogAdapter = new DialogAdapter(partID, DialogDataSet.Dialogs, CharacterDataSet.Characters, startID);
        if (_dialogUIController == null) {
            var canvas = Instantiate(DialogCanvas);
            _dialogUIController = canvas.GetComponent<DialogUIController>();
        }
        var dialog = _dialogAdapter.GetNext();
        _dialogUIController.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage);
    }

    public void JumpTo(int id) {
        if (_dialogAdapter != null) {
            _dialogAdapter.SetCounter(id);
        }
    }
}
