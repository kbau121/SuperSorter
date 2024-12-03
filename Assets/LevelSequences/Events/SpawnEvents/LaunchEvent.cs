using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Events/LaunchEvent")]
public class LaunchEvent : LevelEvent
{
    public int LauncherID = -1;

    public float Strength = 0f;
    public float StrengthRange = 0f;

    public Vector3 Target = Vector3.zero;
    public float TargetRadius = 0f;

    public List<GameObject> Objects;

    protected override void _RunImplementation()
    {
        if (!LevelManager.Instance.Launchers.ContainsKey(LauncherID)) return;
        Launcher launcher = LevelManager.Instance.Launchers[LauncherID];

        int randomObjectIdx = Random.Range(0, Objects.Count);

        launcher.LaunchObject(
            Objects[randomObjectIdx],
            Target, Strength,
            TargetRadius, StrengthRange
            );
    }
}
