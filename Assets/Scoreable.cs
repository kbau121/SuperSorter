using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField]
    private ColorProperty _colorProperty;
    public ColorProperty ColorProperty
    {
        get => _colorProperty;
        set => _colorProperty = value;
    }

    [SerializeField]
    private ShapeProperty _shapeProperty;
    public ShapeProperty ShapeProperty
    {
        get => _shapeProperty;
        set => _shapeProperty = value;
    }

    private bool IsScored = false;

    public void Score(bool success)
    {
        if (IsScored) 
            return;
        IsScored = true;

        Debug.Log((success ? "Successful" : "Failed") + " Score");

        if (success)
        {
            LevelRoot.Instance.AudioManager.PlaySuccessClip(transform.position);
            LevelRoot.Instance.ParticleManager.PlaySuccessParticle(transform.position);
        }

        LevelRoot levelRoot = LevelRoot.Instance;
        if (levelRoot == null)
            return;

        LevelManager levelManager = levelRoot.LevelManager;
        if (levelManager == null)
            return;

        int points = success ? levelManager.SuccessPoints : levelManager.FailurePoints;
        levelManager.ModifyScore(points);
    }

    private void OnDestroy()
    {
        Score(false);
    }
}
