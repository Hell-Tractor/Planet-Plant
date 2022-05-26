using System.Collections.Generic;
using UnityEngine;

namespace Data {

/// <summary>
/// Class for saving character data.
/// </summary>
[System.Serializable]
public class CharacterData {
    public int ID;
    public string Name;
}

/// <summary>
/// DataSet for character data.
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData", order = 2)]
public class CharacterDataSet : ScriptableObject {
    public List<CharacterData> Characters = new List<CharacterData>();
}

}
