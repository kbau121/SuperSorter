using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SelectSceneTransitioner : MonoBehaviour
{
    [SerializeField] private InteractableUnityEventWrapper level1EventWrapper;
    [SerializeField] private LevelSequence level1Sequence;

    [SerializeField] private InteractableUnityEventWrapper level2EventWrapper;
    [SerializeField] private LevelSequence level2Sequence;

    private void Start()
    {
        level1EventWrapper.WhenSelect.AddListener(GoLevel1);
        level2EventWrapper.WhenSelect.AddListener(GoLevel2);
    }

    private void GoLevel1()
    {
        Debug.Log("Going to level 1.");
        GlobalInfo.CurrentLevelSequence = level1Sequence;
        SceneManager.LoadScene("LevelScene");
    }

    private void GoLevel2()
    {
        Debug.Log("Going to level 2.");
        GlobalInfo.CurrentLevelSequence = level2Sequence;
        SceneManager.LoadScene("LevelScene");
    }
}