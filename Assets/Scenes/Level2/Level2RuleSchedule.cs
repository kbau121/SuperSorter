using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RuleSchedules/" + nameof(Level2RuleSchedule))]
public class Level2RuleSchedule : RuleSchedule
{
    public override List<float> RulesChangeTimes => new();

    public override string GetRuleDescription(float timeElapsed)
    {
        return "Note the bouncing floor. Red items go into the blue portal; blue items go into the red portal.";
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
