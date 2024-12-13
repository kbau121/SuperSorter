using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Rules/ShapeRule")]
public class ShapeRule : Rule
{
    public enum Shape
    {
        [InspectorName("Cube")]
        CUBE,
        [InspectorName("Sphere")]
        SPHERE
    }

    [SerializeField]
    private List<ShapeProperty> AllowedShapes = new();

    public override bool Score(Scoreable scoreable)
    {
        return AllowedShapes.Contains(scoreable.ShapeProperty);
    }
}
