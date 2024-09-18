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
    
    [SerializeField] private float NoiseX = 0.3f;
    [SerializeField] private float NoiseZ = 0.3f;
    [SerializeField] private float NoiseY = 2f;

    private float OffsetX = 0f;
    private float OffsetY = 0f;
    [SerializeField] private float OffsetSpeed = 0.05f;

    [SerializeField] private float UV = 0.25f;

    [SerializeField] MovePlayer capsle;

    Vector2[] uv;
    void Update()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();

        //moves the Noise over the mesh
        OffsetY = OffsetY += OffsetSpeed;
        

    }

    void CreateShape()
    {
        vertices = new Vector3[ (xSize + 1) * (zSize + 1) ];
        uv = new Vector2[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for(int z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * NoiseX, z * NoiseZ + OffsetY) * NoiseY;
                vertices[i] = new Vector3(x, y, z);
                uv[i] = new Vector2(x,y) * UV;
                i++;
            }
        }

        triangles = new int[xSize*zSize*6];

        int vert = 0;
        int tris = 0;

        for (int z = 0;z < zSize; z++)
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
    
    void UpdateMesh()
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
