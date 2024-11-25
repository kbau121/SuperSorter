using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Rules/AnyRule")]
public class AnyRule : Rule
{
    [SerializeField]
    private List<Rule> Rules = new List<Rule>();

    public override bool Score(Scoreable scoreable)
    {
        foreach (Rule rule in Rules)
        {
            if (rule.Score(scoreable))
            {
                return true;
            }
        }

        return false;
    }
}
