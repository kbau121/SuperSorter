using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRoot : MonoBehaviour
{
    private static LevelRoot instance;

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

    public AudioManager AudioManager => audioManager;

    private void OnDestroy()
    {
        instance = null;
    }
}
