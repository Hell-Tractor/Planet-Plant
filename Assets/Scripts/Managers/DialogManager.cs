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
            orderby dialog.ID
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
        AdaptedDialog result = new AdaptedDialog();
        var dialog = this._dialogs[id];
        result.SpeakerName = this._characters.Find(x => x.ID == dialog.SpeakerID).Name;
        result.Content = dialog.Content;
        string imgPath = "Drawings/" + result.SpeakerName + "/" + dialog.Emotion;
        result.SpeakerImage = Resources.Load<Sprite>(imgPath);
        if (result.SpeakerName == "路人")
            result.SpeakerName = "???";
        return result;
    }
    public Data.DialogData GetRawDialog(int id) {
        if (id >= this._dialogs.Count) {
            return null;
        }
        return this._dialogs[id];
    }

    public Data.DialogData GetRawDialog() {
        if (this._counter < 1 || this._counter > this._dialogs.Count)
            return null;
        return this._dialogs[this._counter - 1];
    }

    public void MoveNext() {
        this._counter++;
    }

    public int GetCurrentID() {
        return this._counter;
    }

    public int GetPartID() {
        return this._partID;
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
    public List<Canvas> IgnoreCanvas = new List<Canvas>();
    public static DialogManager Instance;
    /// <summary>
    /// 在dialog更新前调用
    /// </summary>
    public event Action<Data.DialogData> BeforeDialogChange;
    public event Action<Data.DialogData> AfterDialogChange;
    public event Action<int> OnDialogEnd;
    private SelectionManager _selectionManager = null;
    private GameObject _selectionUI = null;
    public DialogUIController UI { get; private set; } = null;
    private DialogAdapter _dialogAdapter = null;

    public int testDialog = -1;

    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        if (testDialog != -1)
            this.ShowDialog(testDialog);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (_dialogAdapter != null && !_selectionUI.activeSelf) {
                if (UI?.PreventKeyEventProcessing == true) {
                    return;
                }
                var rawDialog = _dialogAdapter.GetRawDialog();
                Data.SelectionData selection = null;
                if (rawDialog != null) {
                    selection = this._getSelection(rawDialog.ID);
                }
                if (selection != null) {
                    this._selectionManager.ShowSelection(selection.Options);
                    Type handlerType = Type.GetType("SelectionHandler" + selection.ID.ToString());
                    var handler = handlerType == null ? new DefaultSelectionHandler() : Activator.CreateInstance(handlerType) as ISelectionHandler;
                    this._selectionManager.SetHandler(handler);
                } else {
                    this.ShowNext();
                } 
            }
        }
    }

    public void ShowDialog(int partID, int startID = 0) {
        _dialogAdapter = new DialogAdapter(partID, DialogDataSet.Dialogs, CharacterDataSet.Characters, startID);
        if (UI == null) {
            var canvas = Instantiate(DialogCanvas);
            UI = canvas.GetComponent<DialogUIController>();
            _selectionUI = canvas.transform.Find("Selection")?.gameObject ??
                canvas.transform.GetChild(0).Find("Selection")?.gameObject;
            _selectionManager = _selectionUI?.GetComponent<SelectionManager>();
        }
        this.BeforeDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
        var dialog = _dialogAdapter.GetNext();
        UI.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage, _isGraduallyChange(partID, startID));
        this.AfterDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
    }

    public void JumpTo(int id) {
        if (_dialogAdapter != null) {
            _dialogAdapter.SetCounter(id);
        }
    }

    private Data.SelectionData _getSelection(int dialogID) {
        return SelectionDataSet.Selections.Find(x => x.DialogID == dialogID);
    }

    public void ShowNext() {
        if (_dialogAdapter.HasNext()) {
            this.BeforeDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
            var dialog = _dialogAdapter.GetNext();
            UI.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage);
            this.AfterDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
        } else {
            UI.Hide();
            UI = null;
            int id = _dialogAdapter.GetPartID();
            _dialogAdapter = null;
            this.OnDialogEnd?.Invoke(id);
        }
    }

    private bool _isGraduallyChange(int partID, int startID) {
        if (partID == 1 && startID == 0) {
            return false;
        }
        if (partID == 7 && startID == 0) {
            return false;
        }
        return true;
    }
}
