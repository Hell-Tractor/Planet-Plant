using UnityEngine;

public class GlobalProperties : Utils.Singleton<GlobalProperties> {
    [Header("家庭总资产，单位：分")]
    public int FamilyAsset = 200 * 100;
}