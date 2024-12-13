using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuTransitioner : MonoBehaviour
{
    [SerializeField] private InteractableUnityEventWrapper tutorialEventWrapper;
    [SerializeField] private InteractableUnityEventWrapper level1EventWrapper;
    [SerializeField] private InteractableUnityEventWrapper level2EventWrapper;

    private void Start()
    {
        tutorialEventWrapper.WhenSelect.AddListener(GoTutorial);
        level1EventWrapper.WhenSelect.AddListener(GoLevel1);
        level2EventWrapper.WhenSelect.AddListener(GoLevel2);
    }

    private void GoTutorial()
    {
        Debug.Log("Going to the tutorial level.");
        SceneManager.LoadScene("TutorialLevel");
    }

    private void GoLevel1()
    {
        Debug.Log("Going to level 1.");
        SceneManager.LoadScene("Level1");
    }
    private void GoLevel2()
    {
        Debug.Log("Going to level 2.");
        SceneManager.LoadScene("Level2");
    }

}
