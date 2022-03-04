using UnityEngine;
namespace Utils {
public interface ISaveLoad {

    /// <summary>
    /// 保存数据
    /// </summary>
    public void Save();
    /// <summary>
    /// 加载数据
    /// </summary>
    public void Load();
    /// <summary>
    /// 获取保存数据的ScriptableObject
    /// </summary>
    public ScriptableObject GetDataContainer();
}

/// <summary>
/// 拓展ISaveLoad接口
/// </summary>
public static class ExtendISaveLoad {
    /// <summary>
    /// 为当前对象启用自动存储/加载功能
    /// </summary>
    /// <param name="isPreference">标明当前对象是否是Preference</param>
    public static void EnableAutoSaveLoad<T>(this T obj, bool isPreference = false) where T : ISaveLoad {
        SaveLoadManager.Instance.AddItem(obj, isPreference);
    }
    /// <summary>
    /// 为当前对象禁用自动存储/加载功能
    /// </summary>
    /// <param name="isPreference">标明当前对象是否是Preference</param>
    public static void DisableAutoSaveLoad<T>(this T obj, bool isPreference = false) where T : ISaveLoad {
        SaveLoadManager.Instance.RemoveItem(obj, isPreference);
    }
}

}