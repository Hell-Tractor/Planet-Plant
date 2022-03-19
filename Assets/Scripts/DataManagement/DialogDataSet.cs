using System.Collections.Generic;
using UnityEngine;

namespace Data {

[System.Serializable]
public class DialogData {
    public int ID;
    public int PartID;
    public int SpeakerID;
    public string Emotion;
    public string Content;
}

[CreateAssetMenu(fileName = "DialogData", menuName = "Data/DialogData", order = 3)]
public class DialogDataSet : ScriptableObject {
    public List<DialogData> Dialogs = new List<DialogData>();
}

}