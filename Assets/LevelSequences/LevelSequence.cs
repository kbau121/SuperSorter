using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Sequence")]
public class LevelSequence : ScriptableObject
{
    [SerializeField]
    private bool DoLoop = false;

    [SerializeField]
    private LevelEvent[] Events;

    private int NextIndex = 0;

    public void Reset()
    {
        NextIndex = 0;
    }

    public LevelEvent Next()
    {
        if (NextIndex >= Events.Length)
        {
            if (DoLoop) NextIndex %= Events.Length;
            else return null;
        }
        LevelEvent next = Events[NextIndex];

        NextIndex++;
        return next;
    }
}
