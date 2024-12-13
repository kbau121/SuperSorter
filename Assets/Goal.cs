using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Goal : MonoBehaviour
{
    [SerializeField]
    private ColorProperty _colorProperty = ColorProperty.NONE;

    private LevelManager _levelManager;

    private void Start()
    {
        if (_colorProperty == ColorProperty.NONE)
            Debug.LogError($"Unset {nameof(_colorProperty)} for {nameof(Goal)}.");

        _levelManager = LevelRoot.Instance.LevelManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        RuleSchedule ruleSchedule = _levelManager.RuleSchedule;
        Scoreable scoreable = other.gameObject.GetComponentInParent<Scoreable>();

        if (!scoreable) Debug.Log("NULL");

        if (!scoreable) return;

        bool success = ruleSchedule.ItemIsCorrect(scoreable, _colorProperty, _levelManager.TimeElapsed);
        if (!success)
        {
            LevelRoot.Instance.AudioManager.PlayWrongItemClip(transform.position);
        }

        scoreable.Score(success);

        Destroy(scoreable.gameObject);
    }
}
