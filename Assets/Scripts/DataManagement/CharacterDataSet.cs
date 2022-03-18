using System.Collections.Generic;
using UnityEngine;

namespace Data {

[System.Serializable]
public class CharacterData {
    public int ID;
    public string Name;
    public Sprite Image;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData", order = 2)]
public class CharacterDataSet : ScriptableObject {
    public List<CharacterData> Characters = new List<CharacterData>();
}

}
