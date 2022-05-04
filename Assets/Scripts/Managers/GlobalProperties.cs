using UnityEngine;
using Utils;

public class GlobalProperties : Utils.Singleton<GlobalProperties>, Utils.ISaveLoad {
    [Header("家庭总资产，单位：分")]
    public int FamilyAsset = 200 * 100;

    public GlobalProperties() {
        this.EnableAutoSaveLoad(false);
    }
    
    public ScriptableObject GetDataContainer() {
        throw new System.NotImplementedException();
    }

    public void Load() {
        throw new System.NotImplementedException();
    }

    public void Save() {
        throw new System.NotImplementedException();
    }
}