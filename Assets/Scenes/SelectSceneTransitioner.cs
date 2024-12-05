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
    [SerializeField] private InteractableUnityEventWrapper level2EventWrapper;

    private void Start()
    {
        level1EventWrapper.WhenSelect.AddListener(GoLevel1);
        level2EventWrapper.WhenSelect.AddListener(GoLevel2);
    }

    private void GoLevel1()
    {
        Debug.Log("Going to level 1.");
        SceneManager.LoadScene("LevelScene");
    }

    private void GoLevel2()
    {
        Debug.Log("Going to level 2.");
    }
}
