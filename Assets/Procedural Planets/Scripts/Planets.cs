using UnityEngine;

public class Planets : MonoBehaviour
{
    public bool autoUpdate = true;
    [Range(2, 256)] public int resolution = 10;
    public ShapeSetting shapeSetting;
    public ColorSetting colorSetting;
    
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    [HideInInspector] public bool shapeSettingFoldout;
    [HideInInspector] public bool colorSettingFoldout;

    ShapeGenerator shapeGenerator;

    MeshFilter[] meshFilters;
    TerrainFaces[] terrainFaces;


    private void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSetting);

        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];

        terrainFaces = new TerrainFaces[meshFilters.Length];
        Vector3[] direction = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };


        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("Mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFaces(shapeGenerator, meshFilters[i].sharedMesh, resolution, direction[i]);

            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }

    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColorSettingUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < meshFilters.Length; i++) 
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }
    }
    void GenerateColors()
    {
        foreach (MeshFilter mesh in meshFilters)
        {
            mesh.GetComponent<Renderer>().sharedMaterial.color = colorSetting.planetColor;
        }
    }
}
