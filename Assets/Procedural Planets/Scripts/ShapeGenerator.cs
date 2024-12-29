using UnityEngine;

public class ShapeGenerator
{
    ShapeSetting settings;
 
    public ShapeGenerator(ShapeSetting settings)
    {
        this.settings = settings;
    }
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * settings.planetRadius;
    }
}
