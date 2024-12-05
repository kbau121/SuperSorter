using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Events/AlertEvent")]
public class AlertEvent : LevelEvent
{
    public int LauncherID = -1;

    protected override void _RunImplementation()
    {
        LevelManager levelManager = LevelRoot.Instance.LevelManager;

        if (!levelManager.Launchers.ContainsKey(LauncherID))
            return;
        
        Launcher launcher = levelManager.Launchers[LauncherID];
        launcher.Alert();
    }
}
