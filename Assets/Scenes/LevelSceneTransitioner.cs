using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneTransitioner : MonoBehaviour
{

    [SerializeField] private InteractableUnityEventWrapper returnEventWrapper;

    // Start is called before the first frame update
    void Start()
    {
        returnEventWrapper.WhenSelect.AddListener(Return);
    }

    // Update is called once per frame
    void Return()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
