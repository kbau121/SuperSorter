using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RuleSchedules/" + nameof(Level3RuleSchedule))]
public class Level3RuleSchedule : RuleSchedule
{
    public override List<float> RulesChangeTimes => new();

    public override string GetRuleDescription(float timeElapsed)
    {
        return "Red items go into the blue portal; blue items go into the red portal.";
    }

    public override bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColorProperty, float timeElapsed)
    {
        if (scoreable.ColorProperty == ColorProperty.RED)
        {
            return goalColorProperty == ColorProperty.BLUE;
        }
        else if (scoreable.ColorProperty == ColorProperty.BLUE)
        {
            return goalColorProperty == ColorProperty.RED;
        }
        else
        {
            return false;
        }
    }
}
