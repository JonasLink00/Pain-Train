using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    [SerializeField] private int xSize = 20;
    [SerializeField] private int zSize = 20;

    [SerializeField] private float NoiseScaleX = 0.3f;
    [SerializeField] private float NoiseScaleZ = 0.3f;
    [SerializeField] private float NoiseAmpletudeY = 2f;

    [SerializeField] private float OffsetX = 1f;
    [SerializeField] private float OffsetZ = 1f;
    [SerializeField] private float OffsetSpeed = 0.05f;

    [SerializeField] private float UV = 0.25f;

    [SerializeField] bool infinitTerrain = false;

    private Vector3 meshGlobalPosition;

    Vector2[] uv;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }
    void Update()
    {


        if (!infinitTerrain)
        {
            CreateShape();
            UpdateMesh();
            //moves the Noise over the mesh
            OffsetZ = OffsetZ += OffsetSpeed;
        }
        CreateShape();
        UpdateMesh();



    }

    public void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uv = new Vector2[(xSize + 1) * (zSize + 1)];

        if (!infinitTerrain)
        {
            int i = 0;
            for (int z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    float y = Mathf.PerlinNoise(x * NoiseScaleX, z * NoiseScaleZ + OffsetZ) * NoiseAmpletudeY;
                    vertices[i] = new Vector3(x, y, z);
                    uv[i] = new Vector2(x, y) * UV;
                    i++;
                }
            }
        }
        else
        {
            Vector3 meshGlobalPosition = gameObject.transform.TransformDirection(gameObject.transform.position);

            int i = 0;
            for (int z = 0; z <= zSize; z++)
            {
                for (int x = 0; x <= xSize; x++)
                {
                    float y = Mathf.PerlinNoise((x + OffsetX * meshGlobalPosition.x) * NoiseScaleX, (z + OffsetZ * meshGlobalPosition.z) * NoiseScaleZ) * NoiseAmpletudeY;
                    vertices[i] = new Vector3(x, y, z);
                    uv[i] = new Vector2(x, y) * UV;
                    i++;

                }
            }
            //Debug.Log("Change Offset");
        }


        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    public void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.uv = uv;

        mesh.RecalculateNormals();
    }

    //private void OnDrawGizmos()
    //{
    //    if (vertices == null)
    //        return;
    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Gizmos.DrawSphere(vertices[i], 0.1f);
    //    }
    //}





}
