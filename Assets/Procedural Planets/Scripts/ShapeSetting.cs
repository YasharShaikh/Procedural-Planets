using UnityEngine;


[CreateAssetMenu()]
public class ShapeSetting : ScriptableObject
{
    public float planetRadius;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerMask;
        public NoiseSettings noiseSettings;
    }
}
