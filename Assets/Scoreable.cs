using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField]
    private ColorRule.Color _Color;
    public ColorRule.Color Color {
        get
        {
            return _Color;
        }
        private set
        {
            _Color = value;
        }
    }

    [SerializeField]
    private ShapeRule.Shape _Shape;
    public ShapeRule.Shape Shape {
        get
        {
            return _Shape;
        }
        private set
        {
            _Shape = value;
        }
    }

    private bool IsScored = false;

    public void Score(bool success)
    {
        if (IsScored) return;
        IsScored = true;

        Debug.Log((success ? "Successful" : "Failed") + " Score");

        if (success)
        {
            LevelRoot.Instance.AudioManager.PlaySuccessClip(transform.position);
        }

        if (LevelManager.Instance != null)
            LevelManager.Instance.ModifyScore(success);
    }

    private void OnDestroy()
    {
        Score(false);
    }
}
