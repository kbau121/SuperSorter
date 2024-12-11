using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Events/LaunchAtHMDEvent")]
public class LaunchAtHeadsetEvent : LevelEvent
{
    public int LauncherID = -1;

    public float Strength = 0f;
    public float StrengthRange = 0f;

    public Vector3 Offset = Vector3.zero;
    public float TargetRadius = 0f;

    public List<GameObject> Objects;

    protected override void _RunImplementation()
    {
        Vector3 Target = LevelRoot.Instance.HmdTransform.position + Offset;
        LevelManager levelManager = LevelRoot.Instance.LevelManager;

        if (!levelManager.Launchers.ContainsKey(LauncherID))
            return;

        Launcher launcher = levelManager.Launchers[LauncherID];

        int randomObjectIdx = Random.Range(0, Objects.Count);

        launcher.LaunchObject(
            Objects[randomObjectIdx],
            Target, Strength,
            TargetRadius, StrengthRange
        );
    }
}
