using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment : MonoBehaviour
{
    public int Vertices = 0;

    Mesh M;
    int[] indices;

    private void Start()
    {
        M = GetComponent<MeshFilter>().mesh;
        //Vectices = Mathf.RoundToInt(M.vertices.Length / Divisible);
        indices = M.GetTriangles(0);

        StartCoroutine(SplitMesh(true, 0, indices.Length / 2));
        StartCoroutine(SplitMesh(true, indices.Length / 2, indices.Length));
    }

    IEnumerator SplitMesh(bool destroy, int start, int end)
    {
        Material[] materials =  GetComponent<MeshRenderer>().materials;

        int[] indices = M.GetTriangles(0);

        Debug.Log(indices.Length);

        for (int i = start; i < end; i += Vertices)
        {
            Vector3[] newVerts = new Vector3[Vertices];
            Vector3[] newNormals = new Vector3[Vertices];
            Vector2[] newUvs = new Vector2[Vertices];


            for (int n = 0; n < Vertices; ++n)
            {
                if ((i + n) >= end)
                    break;
                int index = indices[i + n];

                newVerts[n] = M.vertices[index];

                newUvs[n] = M.uv[index];
                newNormals[n] = M.normals[index];
            }

            /*Mesh mesh = new Mesh();
            mesh.vertices = newVerts;
            mesh.normals = newNormals;
            mesh.uv = newUvs;

            mesh.triangles = MakeTriangles(Vertices);*/
            
            GameObject GO = new GameObject("Square " + (i / Vertices));

            GO.transform.position = transform.position;
            GO.transform.rotation = transform.rotation;
            GO.transform.localScale = transform.localScale;

            GO.layer = LayerMask.NameToLayer("Ground");
            GO.AddComponent<MeshRenderer>().material = materials[0];

            Mesh GMesh = GO.AddComponent<MeshFilter>().mesh;
            GMesh.Clear();

            GMesh.vertices = newVerts;
            GMesh.normals = newNormals;
            GMesh.uv = newUvs;

            GMesh.triangles = MakeTriangles(Vertices);

            GMesh.RecalculateNormals();
            GMesh.RecalculateTangents();

            GO.AddComponent<MeshCollider>().convex = true;
            //Debug.Log("Square " + (i / Vectices));
            yield return null;
        }
        
        GetComponent<Renderer>().enabled = false;

        if (destroy == true)
        {
            Destroy(gameObject);
        }

    }

    int[] MakeTriangles(int _points)
    {
        int[] points = new int[_points];
        for (int i = 0; i < _points; ++i)
            points[i] = i;
        return points;
    }
}
