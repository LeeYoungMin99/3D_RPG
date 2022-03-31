using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public bool Completed { get; set; }
    public int CurAmount { get; set; }
    public int RequiredAmount { get; set; }

    public event EventHandler<EventArgs> EvaluateEvent;

    public virtual void Init() { }

    protected bool Evaluate()
    {
        if (CurAmount < RequiredAmount) return false;

        Completed = true;

        EvaluateEvent?.Invoke(this, EventArgs.Empty);

        return true;
    }
}
