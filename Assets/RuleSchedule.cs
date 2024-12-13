using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuleSchedule : ScriptableObject
{
    public abstract List<float> RulesChangeTimes { get; }

    public abstract bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColorProperty, float timeElapsed);

    public abstract string GetRuleDescription(float timeElapsed);
}
