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

    private void Start()
    {
        tutorialEventWrapper.WhenSelect.AddListener(GoTutorial);
    }

    private void GoTutorial()

    {
        Debug.Log("Going to the tutorial level.");
        SceneManager.LoadScene("TutorialLevel");
    }
}
