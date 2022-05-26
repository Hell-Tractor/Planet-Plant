using UnityEngine;
using System.Collections.Generic;
using System;

namespace Data {

/// <summary>
/// Class for saving dialog selection data.
/// </summary>
[System.Serializable]
public class SelectionData {
    public int ID;
    public int DialogID;
    public List<string> Options = new List<string>();
}

/// <summary>
/// DataSet of dialog selection data.
/// </summary>
[CreateAssetMenu(fileName = "SelectionData", menuName = "Data/SelectionData", order = 4)]
public class SelectionDataSet : ScriptableObject {
    public List<SelectionData> Selections = new List<SelectionData>();
}

}