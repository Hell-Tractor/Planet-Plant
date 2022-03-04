using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// 时间管理器
/// </summary>
public class TimeManager : MonoBehaviour, ISaveLoad
{
    [SerializeField, Tooltip("起始时间，若加载时间则会被覆盖")]
    public pp.DateTime StartTime;
    [ReadOnly, Tooltip("当前时间")]
    public string CurrentTime;
    public TimeManagerData TimeManagerData;
    [Tooltip("游戏时间与现实时间的倍率")]
    public int TimeRate = 60;
    [Tooltip("每日小时数"), Range(1, 999)]
    public int HourPerDay = 24;
    [Header("游戏显示时间缩放比例")]
    [Range(0, 10)]
    public float HourScale = 1f;
    private bool _isPaused = false;
    private pp.DateTime _currentTime;
    private List<Tuple<pp.DateTime, Action>> _timedTasks = new List<Tuple<pp.DateTime, Action>>();
    /// <summary>
    /// 当日期更新时触发
    /// </summary>
    public event Action OnDayChange;
    
    private void Start() {
        _currentTime = StartTime;
        _currentTime.HourPerDay = HourPerDay;
        CurrentTime = _currentTime.ToString();
        // 启用自动保存/加载
        this.EnableAutoSaveLoad();
    }

    private void Update() {
        if (!_isPaused) {
            int lastDay = _currentTime.Day;
            _currentTime.AddSeconds(Time.deltaTime * TimeRate);
            if (lastDay != _currentTime.Day) {
                OnDayChange?.Invoke();
            }
            CurrentTime = _currentTime.ToString();
            
            // 执行定时任务
            for (int i = 0; i < _timedTasks.Count; ++i) {
                if (_timedTasks[i].Item1 <= _currentTime) {
                    _timedTasks[i].Item2();
                    _timedTasks.RemoveAt(i);
                }
            }
        }
    }
    /// <summary>
    /// 添加定时任务
    /// </summary>
    /// <param name="execTime">任务执行时间</param>
    public void AddTimedTask(pp.DateTime execTime, Action task) {
        if (execTime <= _currentTime) {
            task();
        } else {
            _timedTasks.Add(new Tuple<pp.DateTime, Action>(execTime, task));
        }
    }
    /// <summary>
    /// 暂停时间
    /// </summary>
    public void Pause() {
        _isPaused = true;
    }
    /// <summary>
    /// 恢复时间
    /// </summary>
    public void Resume() {
        _isPaused = false;
    }
    #region ISaveLoad
    public void Save() {
        var data = this.GetDataContainer() as TimeManagerData;
        if (data == null) {
            Debug.LogWarning("TimeManagerData is null");
            return;
        }
        data.CurrentTime = _currentTime;
        data.TimedTasks = _timedTasks;
    }

    public void Load() {
        var data = this.GetDataContainer() as TimeManagerData;
        if (data == null) {
            Debug.LogWarning("TimeManagerData is null");
            return;
        }
        _currentTime = data.CurrentTime;
        _timedTasks = data.TimedTasks;
    }

    public ScriptableObject GetDataContainer() {
        return TimeManagerData;
    }
    #endregion
}