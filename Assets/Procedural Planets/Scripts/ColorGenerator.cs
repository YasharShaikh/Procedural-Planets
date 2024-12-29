using UnityEngine;

public class ColorGenerator 
{
    ColorSetting setting;
    Texture2D texture;
    const int textureResolution = 50;


    public void  UpdateSettings(ColorSetting colorSetting)
    {
        this.setting = colorSetting;
        if(texture == null )
            texture = new Texture2D(textureResolution, 1);
    }   
    public void UpdateElevation(MinMax elevationMinMax)
    {
        setting.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }
    public void UpdateColors()
    {
        Color[] color = new Color[textureResolution];
        for(int i = 0; i < textureResolution; i++)
        {
            color[i] = setting.gradient.Evaluate(i / (textureResolution - 1.0f));   
        }
        texture.SetPixels(color);
        texture.Apply();
        setting.planetMaterial.SetTexture("_Texture", texture);
    }
}
