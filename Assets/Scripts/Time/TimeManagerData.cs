using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerData : ScriptableObject
{
    public UDateTime CurrentTime;
    public List<Tuple<DateTime, Action>> TimedTasks = new List<Tuple<DateTime, Action>>();
}
