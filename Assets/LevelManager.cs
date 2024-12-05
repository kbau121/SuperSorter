using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static LevelSequence;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelSequence levelSequence = null;

    private float EventTimer = 0f;

    [NonSerialized]
    public Dictionary<int, Launcher> Launchers;

    private LevelEvent.RunImplementation EventCallback;

    // Temporary Testing Variables
    public int Score { get; private set; } = 0;
    public float TimeElapsed { get; private set; } = 0.0f;

    void Start()
    {
        Launchers = UnityEngine.Object.FindObjectsOfType<Launcher>().ToDictionary(
            (launcher) => { return launcher.LauncherID; },
            (launcher) => { return launcher; });

        levelSequence.Reset();
    }

    void Update()
    {
        EventTimer -= Time.deltaTime;

        if (EventTimer <= 0f)
        {
            TriggerEvent();
        }

        TimeElapsed += Time.deltaTime;
    }

    private void TriggerEvent()
    {
        if (EventCallback != null)
            EventCallback();

        LevelEvent levelEvent = levelSequence.Next();
        if (levelEvent == null)
        {
            EventTimer = float.PositiveInfinity;
            return;
        }

        EventCallback = levelEvent.Run(out EventTimer);
    }

    public void ModifyScore(bool success)
    {
        Score += success ? 10 : -1;
    }
}
