using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RuleSchedules/" + nameof(Level3RuleSchedule))]
public class Level3RuleSchedule : RuleSchedule
{
    public override List<float> RulesChangeTimes => new() { 30.0f, 60.0f };

    public override string GetRuleDescription(float timeElapsed)
    {
        if (timeElapsed < 30.0f)
        {
            return "Note the conveyor belts! Red items go into the red portal; blue items go into the blue portal.";
        }
        else if (timeElapsed < 60.0f)
        {
            return "Note the conveyor belts! Red items go into the red portal; blue items go into the red portal.";
        }
        else
        {
            return "Note the conveyor belts! Red items go into the blue portal; blue items go into the red portal.";
        }
    }

    public override bool ItemIsCorrect(Scoreable scoreable, ColorProperty goalColorProperty, float timeElapsed)
    {
        if (timeElapsed < 30.0f)
        {
            return scoreable.ColorProperty == goalColorProperty;
        }
        else if (timeElapsed < 60.0f)
        {
            return goalColorProperty == ColorProperty.RED;
        }
        else
        {
            return scoreable.ColorProperty != goalColorProperty;
        }

        
    }
}
