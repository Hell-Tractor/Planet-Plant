using UnityEngine;
using System.Collections.Generic;
using Utils;

/// <summary>
/// 存储/加载管理器
/// </summary>
public class SaveLoadManager : Singleton<SaveLoadManager> {
    private List<ISaveLoad> _autoSaveLoadItemList = new List<ISaveLoad>();
    private List<ISaveLoad> _preferenceList = new List<ISaveLoad>();
    /// <summary>
    /// 向存储/加载管理器中添加项
    /// </summary>
    /// <param name="item">待添加的项</param>
    /// <param name="isPreference">标明待添加项是否是Preference</param>
    /// <returns>存储/加载管理器</returns>
    public SaveLoadManager AddItem(ISaveLoad item, bool isPreference) {
        if (!isPreference && !_autoSaveLoadItemList.Contains(item)) {
            _autoSaveLoadItemList.Add(item);
            return this;
        }
        if (isPreference && !_preferenceList.Contains(item)) {
            _preferenceList.Add(item);
            return this;
        }
        return this;
    }
    /// <summary>
    /// 从存储/加载管理器中移除项
    /// </summary>
    /// <param name="item">待移除的项</param>
    /// <param name="isPreference">标明待移除项是否是Preference</param>
    /// <returns>存储/加载管理器</returns>
    public SaveLoadManager RemoveItem(ISaveLoad item, bool isPreference) {
        if (!isPreference && _autoSaveLoadItemList.Contains(item)) {
            _autoSaveLoadItemList.Remove(item);
            return this;
        }
        if (isPreference && _preferenceList.Contains(item)) {
            _preferenceList.Remove(item);
            return this;
        }
        return this;
    }
    /// <summary>
    /// 保存所有非Preference项
    /// </summary>
    /// <returns>存储/加载管理器</returns>
    public SaveLoadManager SaveItems() {
        foreach (var i in _autoSaveLoadItemList) {
            i.Save();
        }
        return this;
    }
    /// <summary>
    /// 加载所有非Preference项
    /// </summary>
    /// <returns>存储/加载管理器</returns>
    public SaveLoadManager LoadItems() {
        foreach (var i in _autoSaveLoadItemList) {
            i.Load();
        }
        return this;
    }
    /// <summary>
    /// 保存所有Preference项
    /// </summary>
    /// <returns>存储/加载管理器</returns>
    public SaveLoadManager SavePreferences() {
        foreach (var i in _preferenceList) {
            i.Save();
        }
        return this;
    }
    /// <summary>
    /// 加载所有Preference项
    /// </summary>
    /// <returns>存储/加载管理器</returns>
    public SaveLoadManager LoadPreferences() {
        foreach (var i in _preferenceList) {
            i.Load();
        }
        return this;
    }
}