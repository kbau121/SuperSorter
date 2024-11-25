using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Sequence")]
public class LevelSequence : ScriptableObject
{
    [System.Serializable]
    public class LevelEvent
    {
        public string EventName;
        public float Delay;

        public LaunchEvent[] LaunchEvents;
    }

    // TODO Separate event types into scriptable objects similar to rules
    [System.Serializable]
    public class LaunchEvent
    {
        public int LauncherID = -1;

        public float Strength;
        public float StrengthRange = 0f;

        public Vector3 Target;
        public float TargetRadius = 0f;

        public List<GameObject> Objects;
    }

    public bool DoLoop = false;
    public LevelEvent[] Events;
}
