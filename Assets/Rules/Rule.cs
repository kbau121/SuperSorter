using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : ScriptableObject
{
    public abstract bool Score(Scoreable scoreable);
}
