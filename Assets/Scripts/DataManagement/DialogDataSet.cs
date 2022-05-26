using System.Collections.Generic;
using UnityEngine;

namespace Data {

/// <summary>
/// Class for saving dialog data.
/// </summary>
[System.Serializable]
public class DialogData {
    public int ID;
    public int PartID;
    public int SpeakerID;
    public string Emotion;
    public string Content;
}

/// <summary>
/// DataSet for dialog data.
/// </summary>
[CreateAssetMenu(fileName = "DialogData", menuName = "Data/DialogData", order = 3)]
public class DialogDataSet : ScriptableObject {
    public List<DialogData> Dialogs = new List<DialogData>();
}

}