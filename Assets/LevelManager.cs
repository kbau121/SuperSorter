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
    private float EventTimer = 0f;
    private LevelEvent.RunImplementation EventCallback;

    [SerializeField] private LevelSequence _levelSequence = null;
    [SerializeField] private RuleSchedule _ruleSchedule = null;
    [SerializeField] private int _successPoints = 1;
    [SerializeField] private int _failurePoints = -1;
    [SerializeField] private float _durationSeconds = 60;
    [SerializeField] private float _passingThreshold = 0;
    [SerializeField] private string _levelName = "Level";
    


    public LevelSequence LevelSequence => _levelSequence;
    public RuleSchedule RuleSchedule => _ruleSchedule;
    public int SuccessPoints => _successPoints;
    public int FailurePoints => _failurePoints;
    public float LevelDuration => _durationSeconds;
    public float PassingThreshold => _passingThreshold;
    public string LevelName => _levelName;

    // Temporary Testing Variables
    public int Score { get; private set; } = 0;
    public float TimeElapsed { get; private set; } = 0.0f;


    [NonSerialized]
    public Dictionary<int, Launcher> Launchers;


    void Start()
    {
        if (_ruleSchedule == null)
        {
            Debug.LogError($"{nameof(LevelManager)}'s {nameof(_ruleSchedule)} is not set.");
        }

        Launchers = FindObjectsOfType<Launcher>().Where((launcher) => launcher.LauncherID >= 0).ToDictionary(
            (launcher) => { return launcher.LauncherID; },
            (launcher) => { return launcher; });

        _levelSequence.Reset();
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

        LevelEvent levelEvent = _levelSequence.Next();
        if (levelEvent == null)
        {
            EventTimer = float.PositiveInfinity;
            return;
        }

        EventCallback = levelEvent.Run(out EventTimer);
    }

    public void ModifyScore(int amount)
    {
        Score += amount;
    }
}
