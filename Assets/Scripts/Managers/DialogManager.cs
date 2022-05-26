using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A auxiliary class for dialog after adpatation.
/// </summary>
class AdaptedDialog {
    public string SpeakerName { get; set; }
    public string Content { get; set; }
    public Sprite SpeakerImage { get; set; }
}

/// <summary>
/// A class for converting raw dialog into adapted dialog.
/// </summary>
class DialogAdapter {
    private int _partID;
    private List<Data.DialogData> _dialogs = new List<Data.DialogData>();
    private List<Data.CharacterData> _characters = new List<Data.CharacterData>();
    private int _counter;
    /// <summary>
    /// create a dialog adapter with given dialog id.
    /// </summary>
    /// <param name="partid">dialog part id</param>
    /// <param name="dialogs">All dialog datas</param>
    /// <param name="characters">All character datas</param>
    /// <param name="startID">the id in current dialog, default 0</param>
    public DialogAdapter(int partid, List<Data.DialogData> dialogs, List<Data.CharacterData> characters, int startID = 0) {
        this._counter = startID;
        this._partID = partid;

        // select all dialogs with given part id, and sort by dialog id
        this._dialogs = (
            from dialog in dialogs
            where dialog.PartID == partid
            orderby dialog.ID
            select dialog
        ).ToList();

        this._characters = characters;
    }

    /// <summary>
    /// Get the next adapted dialog
    /// </summary>
    /// <returns>next adapted dialog, might be null</returns>
    public AdaptedDialog GetNext() {
        if (this._counter >= this._dialogs.Count) {
            return null;
        }
        return GetDialog(this._counter++);
    }

    /// <summary>
    /// Get adatped dialog with given id
    /// </summary>
    /// <param name="id">the index of the dialog in current part</param>
    /// <returns>adapted dialog, might be null</returns>
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

    /// <summary>
    /// Get raw dialog with given id
    /// </summary>
    /// <param name="id">the index of the dialog in current part</param>
    /// <returns>raw dialog, might be null</returns>
    public Data.DialogData GetRawDialog(int id) {
        if (id >= this._dialogs.Count) {
            return null;
        }
        return this._dialogs[id];
    }

    /// <summary>
    /// Get raw dialog of current dialog
    /// </summary>
    /// <returns>raw dialog, might be null</returns>
    public Data.DialogData GetRawDialog() {
        if (this._counter < 1 || this._counter > this._dialogs.Count)
            return null;
        return this._dialogs[this._counter - 1];
    }

    /// <summary>
    /// Skip current dialog
    /// </summary>
    public void MoveNext() {
        this._counter++;
    }

    /// <summary>
    /// Get the index of the dialog in current part
    /// </summary>
    public int GetCurrentID() {
        return this._counter;
    }

    /// <summary>
    /// Get the partID of current part
    /// </summary>
    public int GetPartID() {
        return this._partID;
    }
    
    /// <summary>
    /// Check if current dialog is the last one
    /// </summary>
    /// <returns>true if isn't last one</returns>
    public bool HasNext() {
        return this._counter < this._dialogs.Count;
    }

    /// <summary>
    /// Force to set the current dialog to given id
    /// </summary>
    /// <param name="count">the index of the dialog in current part</param>
    public void SetCounter(int count) {
        this._counter = count;
    } 
}

/// <summary>
/// Class for managing dialogs.
/// </summary>
public class DialogManager : MonoBehaviour {
    // datasets
    public Data.CharacterDataSet CharacterDataSet;
    public Data.DialogDataSet DialogDataSet;
    public Data.SelectionDataSet SelectionDataSet;

    public Canvas DialogCanvas;
    [Tooltip("Dialogs which will not be close during dialog showing")]
    public List<Canvas> IgnoreCanvas = new List<Canvas>();
    public static DialogManager Instance;
    /// <summary>
    /// 在dialog更新前调用
    /// </summary>
    public event Action<Data.DialogData> BeforeDialogChange;
    /// <summary>
    /// 在dialog更新后调用
    /// </summary>
    public event Action<Data.DialogData> AfterDialogChange;
    /// <summary>
    /// 在当前part dialog结束后调用
    /// </summary>
    public event Action<int> OnDialogEnd;
    
    private SelectionManager _selectionManager = null;
    private GameObject _selectionUI = null;
    public DialogUIController UI { get; private set; } = null;
    private DialogAdapter _dialogAdapter = null;

    // * test only
    public int testDialog = -1;

    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        if (testDialog != -1)
            this.ShowDialog(testDialog);
    }

    private void Update() {
        // update dialog when mouse is clicked
        if (Input.GetMouseButtonDown(0)) {
            // check if there is a dialog showing
            if (_dialogAdapter != null && !_selectionUI.activeSelf) {
                // if Prevent key Event Processing, skip
                if (UI?.PreventKeyEventProcessing == true) {
                    return;
                }
                // check if there is a selection after current dialog
                var rawDialog = _dialogAdapter.GetRawDialog();
                Data.SelectionData selection = null;
                if (rawDialog != null) {
                    selection = this._getSelection(rawDialog.ID);
                }
                if (selection != null) {
                    // if there is a selection, show selection UI
                    // use reflection to get corresponding selection manager
                    this._selectionManager.ShowSelection(selection.Options);
                    Type handlerType = Type.GetType("SelectionHandler" + selection.ID.ToString());
                    // if handlerType is null, use default handler
                    var handler = handlerType == null ? new DefaultSelectionHandler() : Activator.CreateInstance(handlerType) as ISelectionHandler;
                    this._selectionManager.SetHandler(handler);
                } else {
                    // no selection, move to next dialog
                    this.ShowNext();
                } 
            }
        }
    }

    /// <summary>
    /// Start a new dialog
    /// </summary>
    /// <param name="partID">part ID of the dialog to be shown</param>
    /// <param name="startID">start index in given part, default 0</param>
    public void ShowDialog(int partID, int startID = 0) {
        // create dialog adapter to manage dialog data
        _dialogAdapter = new DialogAdapter(partID, DialogDataSet.Dialogs, CharacterDataSet.Characters, startID);
        // if no dialog UI exists, create one
        if (UI == null) {
            var canvas = Instantiate(DialogCanvas);
            UI = canvas.GetComponent<DialogUIController>();
            _selectionUI = canvas.transform.Find("Selection")?.gameObject ??
                canvas.transform.GetChild(0).Find("Selection")?.gameObject;
            _selectionManager = _selectionUI?.GetComponent<SelectionManager>();
        }
        // call event
        this.BeforeDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
        
        // show current dialog
        var dialog = _dialogAdapter.GetNext();
        UI.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage, _isGraduallyChange(partID, startID));
        
        // call event
        this.AfterDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
    }

    /// <summary>
    /// Force to show dialog with given id
    /// </summary>
    /// <param name="id">the index of the dialog in current part</param>
    public void JumpTo(int id) {
        if (_dialogAdapter != null) {
            _dialogAdapter.SetCounter(id);
        }
    }

    /// <summary>
    /// Get selection data after current dialog
    /// </summary>
    /// <param name="dialogID">ID of current dialog</param>
    /// <returns>selection data, might be null</returns>
    private Data.SelectionData _getSelection(int dialogID) {
        return SelectionDataSet.Selections.Find(x => x.DialogID == dialogID);
    }

    /// <summary>
    /// Show the next dialog of current part
    /// </summary>
    public void ShowNext() {
        if (_dialogAdapter.HasNext()) {
            // has next dialog, show it
            this.BeforeDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
            var dialog = _dialogAdapter.GetNext();
            UI.SetDialog(dialog.SpeakerName, dialog.Content, dialog.SpeakerImage);
            this.AfterDialogChange?.Invoke(_dialogAdapter.GetRawDialog());
        } else {
            // no dialog left, clean up
            UI.Hide();
            UI = null;
            int id = _dialogAdapter.GetPartID();
            _dialogAdapter = null;
            // call event
            this.OnDialogEnd?.Invoke(id);
        }
    }

    /// <summary>
    /// Set given dialog's type is changed gradually or not
    /// </summary>
    /// <param name="partID">the partID of given dialog</param>
    /// <param name="startID">the index of given dialog in given part</param>
    /// <returns>true if gradually</returns>
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
