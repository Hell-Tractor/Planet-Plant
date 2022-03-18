using UnityEngine;
using System.Collections.Generic;
using System;

namespace Data {

[System.Serializable]
public class SelectionData {
    public int ID;
    public int DialogID;
    public List<Tuple<string, Action>> Options = new List<Tuple<string, Action>>();
}

[CreateAssetMenu(fileName = "SelectionData", menuName = "Data/SelectionData", order = 4)]
public class SelectionDataSet : ScriptableObject {
    public List<SelectionData> selections = new List<SelectionData>();
}

}