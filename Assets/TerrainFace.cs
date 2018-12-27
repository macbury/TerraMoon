using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    private Mesh mesh;
    private Vector3 localUp;
    private int resolution;
    private Vector3 axisA;
    private Vector3 axisB;

    const int RECT_VERTEX_COUNT = 6;

    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        this.axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        this.axisB = Vector3.Cross(localUp, axisA);
    }

    public void Assemble()
    {
        int indexexResolution = resolution - 1;
        Vector3[] verticies = new Vector3[resolution * resolution];
        int[] triangles = new int[indexexResolution * indexexResolution * RECT_VERTEX_COUNT];
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / indexexResolution;
                Vector3 pointOnUnitCube = localUp + 
                                            (percent.x - .5f) * 2 * axisA +
                                            (percent.y - .5f) * 2 * axisB;

                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                verticies[i] = pointOnUnitSphere;

                if (x != indexexResolution && y != indexexResolution)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += RECT_VERTEX_COUNT;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
