using Godot;

[Tool]
public partial class ProceduralPlane : MeshInstance3D
{
	[Export]
	public int Resolution = 10;

	[Export]
	public int Height = 10;

	[Export]
	public int WidthSegments = 1;

	[Export]
	public int HeightSegments = 1;
	Vector3 localUp = Vector3.Up;
	Vector3 axisA;
	Vector3 axisB;

	public override void _Ready()
	{
		base._Ready();
		GD.Print("ahhhhh1");

		axisA = new Vector3(localUp.Y, localUp.Z, localUp.X);
		axisB = localUp.Cross(axisA);


		GeneratePlane();
	}

	private void GeneratePlane()
	{
		ArrayMesh a_mesh = new ArrayMesh();
		Vector3[] vertices = new Vector3[Resolution * Resolution];
		int[] triangles = new int[(Resolution - 1) * (Resolution - 1) * 6];
		int triIndex = 0;

		for (int x = 0; x < Resolution; x++)
		{
			for (int y = 0; y < Resolution; y++)
			{
				int i = x + y * Resolution;
				Vector2 percent = new Vector2(x, y) / (Resolution - 1);
				Vector3 pointOnUnitCube = Vector3.Up + (percent.X - .5f) * 2 * axisA + (percent.Y - .5f) * 2 * axisB;
				// Vector3 pointOnUnitSphere = pointOnUnitCube.Normalized();
				if (x != Resolution - 1 && y != Resolution - 1)
				{
					triangles[triIndex] = i;
					triangles[triIndex + 1] = i + Resolution + 1;
					triangles[triIndex + 2] = i + Resolution;

					triangles[triIndex + 3] = i;
					triangles[triIndex + 4] = i + 1;
					triangles[triIndex + 5] = i + Resolution + 1;
					triIndex += 6;
				}
			}
		}

		var arrays = new Godot.Collections.Array();
		arrays.Resize((int)ArrayMesh.ArrayType.Max);
		arrays[(int)ArrayMesh.ArrayType.Vertex] = vertices;
		arrays[(int)ArrayMesh.ArrayType.Index] = triangles;

		a_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
		Mesh = a_mesh;
	}
}
