using UnityEngine;
using Utils;

/// <summary>
/// Class for saving global properties
/// </summary>
public class GlobalProperties : Utils.Singleton<GlobalProperties>, Utils.ISaveLoad {
    // unit: cent
    public int FamilyAsset = 200 * 100;
    public int PlayerAsset = 15;
    // player properties
    public int PlayerIntelligence;
    public int PlayerPhysique;
    public bool isFirstTimeToField = true;
    public bool isFirstTimeToGoingMarket = true;
    public bool isFristTimeToMarket = true;
    public bool isFirstTimeGoHome = true;
    public bool goHomeDialogShowed = false;

    public GlobalProperties() {
        this.EnableAutoSaveLoad(false);
    }
    
    // todo: to be finished
    #region ISaveLoad
    public ScriptableObject GetDataContainer() {
        throw new System.NotImplementedException();
    }

    public void Load() {
        throw new System.NotImplementedException();
    }

    public void Save() {
        throw new System.NotImplementedException();
    }
    #endregion
}