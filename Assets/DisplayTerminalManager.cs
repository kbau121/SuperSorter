using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTerminalManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text rulesText;
    [SerializeField]
    private Text timeText;

    private void Update()
    {
        LevelManager levelManager = LevelRoot.Instance.LevelManager;

        scoreText.text = $"Score: {levelManager.Score}";

        int secondsElapsed = Math.Max(0, (int)levelManager.TimeElapsed);
        timeText.text = $"Time: {secondsElapsed / 60}:{secondsElapsed % 60:D2}";
    }
}
