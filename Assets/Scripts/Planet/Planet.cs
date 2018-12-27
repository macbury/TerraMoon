using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private MeshFilter[] meshFilters;
    private TerrainFace[] terrainFaces;

    [Range(2, 256)]
    public int resolution = 10;

    [Range(0, -20)]
    public int gravity = -12;

    private const int FaceNumber = 6;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
        if (meshFilters == null)
        {
            meshFilters = new MeshFilter[FaceNumber];
        }

        terrainFaces = new TerrainFace[FaceNumber];

        Vector3[] Directions = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        for (int i = 0; i < FaceNumber; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");
                meshObject.transform.parent = transform;

                MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

                MeshCollider meshCollider = meshObject.AddComponent<MeshCollider>();
            }

            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, Directions[i]);
        }
    }

    public void Attract(Rigidbody rigidbody, Transform bodyTransform)
    {
        Vector3 gravityUp = (bodyTransform.position - transform.position).normalized;
        Vector3 localUp = bodyTransform.up;

        rigidbody.AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * bodyTransform.rotation;
        bodyTransform.rotation = targetRotation;
    }

    void GenerateMesh()
    {
        foreach(TerrainFace face in terrainFaces)
        {
            face.Assemble();
        }
    }
}
