using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for saving time manager data.
/// </summary>
[CreateAssetMenu(fileName = "TimeManagerData", menuName = "Data/TimeManagerData", order = 1)]
public class TimeManagerData : ScriptableObject
{
    public pp.DateTime CurrentTime;
    public List<Tuple<pp.DateTime, Action>> TimedTasks = new List<Tuple<pp.DateTime, Action>>();
    public bool IsPaused;
    public int TimeRate;
}
