using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuleSchedule : ScriptableObject
{
    public abstract bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColor, float timeElapsed);
}