using Meta.XR.ImmersiveDebugger.UserInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTerminalManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _rulesText;
    [SerializeField] private Text _timeText;

    private float _numActiveLaunchers;
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = LevelRoot.Instance.LevelManager;

        LevelSequence levelSequence = _levelManager.LevelSequence;
        HashSet<int> activeLauncherIds = new();
        foreach (LevelEvent levelEvent in levelSequence.LevelEvents)
        {
            if (levelEvent is LaunchEvent launchEvent)
            {
                activeLauncherIds.Add(launchEvent.LauncherID);
            }
        }
        _numActiveLaunchers = activeLauncherIds.Count;
    }

    private void Update()
    {
        LevelManager levelManager = LevelRoot.Instance.LevelManager;
        float timeElapsed = Math.Max(0, levelManager.TimeElapsed);

        if (timeElapsed < levelManager.LevelDuration)
        {
            _scoreText.text = $"Score: {levelManager.Score} / {levelManager.PassingThreshold}";
        }
        else
        {
            if (levelManager.Score >= levelManager.PassingThreshold)
                _scoreText.text = "LEVEL PASSED";
            else
                _scoreText.text = "LEVEL FAILED";
        }

        float timeRemaining = Math.Max(0, levelManager.LevelDuration - timeElapsed);
        int intSecondsRemaining = (int) timeRemaining;

        _timeText.text = $"Time Left: {intSecondsRemaining / 60}:{intSecondsRemaining % 60:D2}";

        string rulesDesc = levelManager.RuleSchedule.GetRuleDescription(timeElapsed);
        _rulesText.text = $"{levelManager.LevelName}\n{_numActiveLaunchers} active launchers\n{rulesDesc}";
    }
}
