using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRuleChangeMonitor : MonoBehaviour
{
    private LevelManager _levelManager;
    private List<float> _changeTimes = new();

    [SerializeField] private AudioClip _rulesChangedClip;
    [SerializeField] private Transform _rulesChangedClipLocation;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = LevelRoot.Instance.LevelManager;
        _changeTimes.AddRange(_levelManager.RuleSchedule.RulesChangeTimes);
        _changeTimes.Sort((a, b) => b.CompareTo(a));  // last element is earliest time
    }

    private void FixedUpdate()
    {
        if (_changeTimes.Count == 0)
            return;

        float timeElapsed = _levelManager.TimeElapsed;
        if (timeElapsed > _changeTimes[_changeTimes.Count - 1])
        {
            _changeTimes.RemoveAt(_changeTimes.Count - 1);

            AudioSource.PlayClipAtPoint(_rulesChangedClip, _rulesChangedClipLocation.position);
        }
    }
}
