using Godot;

public partial class ProceduralPlaneGemini : MeshInstance3D
{

    private Mesh mesh;

    public override void _Ready()
    {
        GeneratePlaneMesh(10);
        //MeshDataTool tool = new MeshDataTool();
        //tool.CreateFromMesh(mesh);
        //SetMesh(tool.GetMesh());
    }

    private void GeneratePlaneMesh(int resolution)
    {
        // Create a new mesh
        ArrayMesh planeMesh = new ArrayMesh();

        // Define vertices based on resolution
        int vertexCount = resolution * resolution;
        Vector3[] vertices = new Vector3[vertexCount];

        float size = 1.0f; // Adjust size as needed
        float step = size / (resolution - 1);

        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                float xPos = x * step;
                float yPos = 0; // Flat plane, adjust for heightmap
                float zPos = z * step;
                vertices[x * resolution + z] = new Vector3(xPos, yPos, zPos);
            }
        }

        // Define UV coordinates (optional)
        Vector2[] uv = new Vector2[vertexCount];
        for (int x = 0; x < resolution; x++)
        {
            for (int z = 0; z < resolution; z++)
            {
                uv[x * resolution + z] = new Vector2((float)x / (resolution - 1), (float)z / (resolution - 1));
            }
        }

        // Define triangles (indices)
        int[] triangles = new int[6 * (resolution - 1) * (resolution - 1)];
        int triIndex = 0;
        for (int x = 0; x < resolution - 1; x++)
        {
            for (int z = 0; z < resolution - 1; z++)
            {
                int bottomLeft = x * resolution + z;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + resolution;
                int topRight = topLeft + 1;

                // First triangle
                triangles[triIndex++] = bottomLeft;
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = bottomRight;

                // Second triangle
                triangles[triIndex++] = bottomRight;
                triangles[triIndex++] = topLeft;
                triangles[triIndex++] = topRight;
            }
        }


        //planeMesh.SetArrays(Mesh.ArrayType.Vertex, vertices);
        //planeMesh.SetArrays(Mesh.ArrayType.Uv, uv); // Optional UV coordinates
        //planeMesh.SetIndices(triangles);

        var arrays = new Godot.Collections.Array();
        arrays.Resize((int)ArrayMesh.ArrayType.Max);
        arrays[(int)ArrayMesh.ArrayType.Vertex] = vertices;
        arrays[(int)ArrayMesh.ArrayType.Index] = triangles;
        arrays[(int)ArrayMesh.ArrayType.TexUV] = uv;

        planeMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
        Mesh = planeMesh;
    }
}