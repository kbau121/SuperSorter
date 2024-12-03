using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelEvent : ScriptableObject
{
    public delegate void RunImplementation();

    public string EventName;
    public float Delay;

    public RunImplementation Run(out float timer)
    {
        timer = Delay;

        return new RunImplementation(_RunImplementation);
    }

    protected abstract void _RunImplementation();
}
