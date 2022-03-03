using System;
using System.Collections.Generic;
using UnityEngine;

// TODO 创建自定义每日小时数的时间类并予以替换
public class TimeManager : MonoBehaviour, Utils.ISaveLoad
{
    [SerializeField, Tooltip("起始时间，若加载时间则会被覆盖")]
    public UDateTime StartTime;
    [ReadOnly, Tooltip("当前时间")]
    public string CurrentTime;
    public TimeManagerData TimeManagerData;
    [Tooltip("游戏时间与现实时间的倍率")]
    public int TimeRate = 60;
    [Header("游戏显示时间缩放比例")]
    [Range(0, 10)]
    public float HourScale = 1f;
    private bool _isPaused = false;
    private DateTime _currentTime;
    private List<Tuple<DateTime, Action>> _timedTasks = new List<Tuple<DateTime, Action>>();
    public event Action OnDayChange;
    
    private void Start() {
        _currentTime = StartTime.dateTime;
        CurrentTime = String.Format("{0}/{1}/{2} {3}:{4}:{5}", _currentTime.Year, _currentTime.Month, _currentTime.Day, _currentTime.Hour * HourScale, _currentTime.Minute, _currentTime.Second);
    }

    private void Update() {
        if (!_isPaused) {
            int lastDay = _currentTime.Day;
            _currentTime = _currentTime.AddSeconds(Time.deltaTime * TimeRate);
            if (lastDay != _currentTime.Day) {
                OnDayChange?.Invoke();
            }
            CurrentTime = String.Format("{0}/{1}/{2} {3}:{4}:{5}", _currentTime.Year, _currentTime.Month, _currentTime.Day, _currentTime.Hour * HourScale, _currentTime.Minute, _currentTime.Second);
            
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
    public void AddTimedTask(DateTime execTime, Action task) {
        if (execTime <= _currentTime) {
            task();
        } else {
            _timedTasks.Add(new Tuple<DateTime, Action>(execTime, task));
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

    public void Save(ScriptableObject originData) {
        var data = originData as TimeManagerData;
        if (data == null) {
            Debug.LogWarning("TimeManagerData is null");
            return;
        }
        data.CurrentTime = _currentTime;
        data.TimedTasks = _timedTasks;
    }

    public void Load(ScriptableObject originData) {
        var data = originData as TimeManagerData;
        if (data == null) {
            Debug.LogWarning("TimeManagerData is null");
            return;
        }
        _currentTime = data.CurrentTime;
        _timedTasks = data.TimedTasks;
    }
}
