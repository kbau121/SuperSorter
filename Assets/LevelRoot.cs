using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRoot : MonoBehaviour
{
    private static LevelRoot instance = null;

    public static LevelRoot Instance
    {
        get
        {
            if (instance == null)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    foreach (GameObject rootGameObject in scene.GetRootGameObjects())
                    {
                        LevelRoot levelRoot = rootGameObject.GetComponent<LevelRoot>();
                        if (levelRoot != null)
                        {
                            instance = levelRoot;
                            return instance;
                        }
                    }
                }
            }

            return instance;
        }
    }

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Transform hmdTransform;

    public AudioManager AudioManager => audioManager;
    public LevelManager LevelManager => levelManager;
    public Transform HmdTransform => hmdTransform;

    /// <summary>
    /// Use late script execution order so that this runs after other OnDestroy's.
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
