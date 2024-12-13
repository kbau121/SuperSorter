using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RuleSchedules/" + nameof(Level2RuleSchedule))]
public class Level2RuleSchedule : RuleSchedule
{
    public override List<float> RulesChangeTimes => new();

    public override string GetRuleDescription(float timeElapsed)
    {
        return "Note the bouncing floor. Round objects go into the blue portal; other items go into the red portal.";
    }

    public override bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColorProperty, float timeElapsed)
    {
        if (goalColorProperty == ColorProperty.BLUE)
        {
            return scoreable.ShapeProperty == ShapeProperty.Sphere;
        }
        else if (goalColorProperty == ColorProperty.RED)
        {
            return scoreable.ShapeProperty != ShapeProperty.Sphere;
        }

        return false;
    }
}
