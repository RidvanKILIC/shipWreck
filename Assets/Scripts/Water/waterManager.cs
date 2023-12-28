using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class waterManager : MonoBehaviour
{
    private MeshFilter _meshFilter;
    Mesh mesh;
    Vector3[] vertices;
    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        //CreateMeshLowPoly(_meshFilter);
    }
    MeshFilter CreateMeshLowPoly(MeshFilter mf)
    {
        mesh = mf.sharedMesh;

        //Get the original vertices of the gameobject's mesh
        Vector3[] originalVertices = mesh.vertices;

        //Get the list of triangle indices of the gameobject's mesh
        int[] triangles = mesh.triangles;

        //Create a vector array for new vertices 
        Vector3[] vertices = new Vector3[triangles.Length];

        //Assign vertices to create triangles out of the mesh
        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = originalVertices[triangles[i]];
            triangles[i] = i;
        }

        //Update the gameobject's mesh with new vertices
        mesh.vertices = vertices;
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.vertices = mesh.vertices;

        return mf;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;
        for(int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = waveManager.instance.getWaveHeight(transform.position.x + vertices[i].x);
        }
        //_meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.RecalculateNormals();
        //mesh.MarkDynamic();
        //_meshFilter.mesh = mesh;
    }
}
