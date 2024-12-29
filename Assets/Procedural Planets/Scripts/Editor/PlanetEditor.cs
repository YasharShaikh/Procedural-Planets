using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planets))]
public class PlanetEditor : Editor
{
    Planets planets;
    Editor shapeEditor;
    Editor colorEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
                planets.GeneratePlanet();
        }
        if (GUILayout.Button("Generate Planet")) ;
        {
            planets.GeneratePlanet();
        }
        DrawSettingsEditor(planets.shapeSetting, planets.OnShapeSettingUpdated, ref planets.shapeSettingFoldout, ref shapeEditor);
        DrawSettingsEditor(planets.colorSetting, planets.OnColorSettingUpdated, ref planets.colorSettingFoldout, ref colorEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingUpdated, ref bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {

                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingUpdated != null)
                            onSettingUpdated();
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        planets = (Planets)target;
    }
}
