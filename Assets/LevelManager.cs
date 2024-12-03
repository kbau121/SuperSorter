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
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private LevelSequence LevelSequence;
    private float EventTimer = 0f;

    [NonSerialized]
    public Dictionary<int, Launcher> Launchers;

    private LevelEvent.RunImplementation EventCallback;

    // Temporary Testing Variables
    private int score = 0;
    [SerializeField]
    private Text text;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Launchers = UnityEngine.Object.FindObjectsOfType<Launcher>().ToDictionary(
            (launcher) => { return launcher.LauncherID; },
            (launcher) => { return launcher; });

        LevelSequence.Reset();
    }

    void Update()
    {
        EventTimer -= Time.deltaTime;

        if (EventTimer <= 0f)
        {
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        if (EventCallback != null) EventCallback();

        LevelEvent levelEvent = LevelSequence.Next();
        if (levelEvent == null)
        {
            EventTimer = float.PositiveInfinity;
            return;
        }

        EventCallback = levelEvent.Run(out EventTimer);
    }

    public void Score(bool success)
    {
        if (success) score += 10;
        else score -= 1;

        text.text = score.ToString();
    }
}
