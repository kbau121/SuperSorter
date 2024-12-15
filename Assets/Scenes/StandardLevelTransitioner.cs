using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandardLevelTransitioner : MonoBehaviour
{
    [SerializeField] private InteractableUnityEventWrapper _returnEventWrapper;

    // Start is called before the first frame update
    void Start()
    {
        _returnEventWrapper.WhenSelect.AddListener(ReturnToMainMenu);
    }

    void ReturnToMainMenu()
    {
        if (LevelRoot.Instance.LevelManager.TimeElapsed > 1.0)
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
