using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Events/AlertEvent")]
public class AlertEvent : LevelEvent
{
    public int LauncherID = -1;

    protected override void _RunImplementation()
    {
        if (!LevelManager.Instance.Launchers.ContainsKey(LauncherID)) return;
        Launcher launcher = LevelManager.Instance.Launchers[LauncherID];

        launcher.Alert();
    }
}
