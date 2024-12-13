using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RuleSchedules/" + nameof(TutorialRuleSchedule))]
public class TutorialRuleSchedule : RuleSchedule
{
    public override List<float> RulesChangeTimes => new();

    public override string GetRuleDescription(float timeElapsed)
    {
        return "Throw each cube into the goal of the same color.";
    }

    public override bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColorProperty, float timeElapsed)
    {
        return scoreable.ColorProperty == goalColorProperty;
    }
}
