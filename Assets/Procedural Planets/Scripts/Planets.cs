using UnityEngine;

public class Planets : MonoBehaviour
{

    [Range(2, 256)] public int resolution = 10;


    MeshFilter[] meshFilters;
    TerrainFaces[] terrainFaces;


    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    private void Initialize()
    {
        if (meshFilters == null || meshFilters.Length == 0)
            meshFilters = new MeshFilter[6];

        terrainFaces = new TerrainFaces[meshFilters.Length];
        Vector3[] direction = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };


        for (int i = 0; i < meshFilters.Length; i++)
        {
            if(meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("Mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFaces(meshFilters[i].sharedMesh, resolution, direction[i]);
        }

    }


    void GenerateMesh()
    {
        foreach (TerrainFaces face in terrainFaces)
        {
            face.ConstructMesh();

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
