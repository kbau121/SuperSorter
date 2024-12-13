using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RuleSchedules/" + nameof(Level1RuleSchedule))]
public class Level1RuleSchedule : RuleSchedule
{
    public override List<float> RulesChangeTimes => new() { 45.0f };

    public override string GetRuleDescription(float timeElapsed)
    {
        if (timeElapsed < 45.0f)
        {
            return $"Red items go to the green portal, green items go to the blue portal, blue items go to the red portal.";
        }
        else
        {
            return $"Red items go to the blue portal, green items go to the red portal, blue items go to the green portal.";
        }
    }

    public override bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColorProperty, float timeElapsed)
    {
        if (timeElapsed < 45.0f)
        {
            if (scoreable.ColorProperty == ColorProperty.RED && goalColorProperty == ColorProperty.GREEN)
                return true;
            else if (scoreable.ColorProperty == ColorProperty.GREEN && goalColorProperty == ColorProperty.BLUE)
                return true;
            else if (scoreable.ColorProperty == ColorProperty.BLUE && goalColorProperty == ColorProperty.RED)
                return true;
            else
                return false;
        }
        else
        {
            if (scoreable.ColorProperty == ColorProperty.RED && goalColorProperty == ColorProperty.BLUE)
                return true;
            else if (scoreable.ColorProperty == ColorProperty.GREEN && goalColorProperty == ColorProperty.RED)
                return true;
            else if (scoreable.ColorProperty == ColorProperty.BLUE && goalColorProperty == ColorProperty.GREEN)
                return true;
            else
                return false;
        }
    }
}
