using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Material = UnityEngine.Material;

public class Ball : MonoBehaviour
{
    public Material mat;
    public float radius = 1f;
    public int pointsCount = 8;

    
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    Mesh mesh;
    
    void InitMeshRenderer()
    {
        // material
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = !mat ? new Material(Shader.Find("Standard")) : mat;
    }
    void Awake()
    {
        InitMeshRenderer();
        GenerateMesh();
    }

    void GenerateMesh()
    {
        mesh = new Mesh();
        meshFilter = gameObject.AddComponent<MeshFilter>();
        
        const float tau = math.PI * 2f;

        var verts = new List<Vector3> {new Vector2(0, 0)};
            //(Vector2)transform.position}; // initialize with center point
        
        // points around
        for (float angle = 0f; angle < tau; angle += tau / (float) pointsCount)
        {
            var vertPos = new Vector3(radius * math.cos(angle), radius * math.sin(angle), 0f);
            verts.Add(vertPos);
        }
        mesh.vertices = verts.ToArray();

        var tris = new List<int>();
        for (int i = 1; i < verts.Count - 1; i++)
        {
            tris.Add(0);
            tris.Add(i);
            tris.Add(i + 1);
        }
        mesh.triangles = tris.ToArray();

        // 2D
        var normals = new List<Vector3>();
        verts.ForEach(vert => normals.Add(Vector3.forward));

        meshFilter.mesh = mesh;
    }
}
