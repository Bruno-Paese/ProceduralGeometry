using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

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

	public override void _Ready()
	{
		base._Ready();
		GD.Print("ahhhhh1");
		GeneratePlane();
	}

	private void GeneratePlane()
	{
		ArrayMesh a_mesh = new ArrayMesh();
		List<Vector3> vertices = new List<Vector3>();
		List<int> indices = new List<int>();

		for (int x = 0; x < Resolution; x++)
		{
			for (int y = 0; y < Resolution; y++)
			{
				vertices.Add(new Vector3(x, 0, y));
				indices.Add((x* Resolution) + y);
				indices.Add(x*Width + y -1);
				indices.Add(x*Width + y);
			}
		}

		var arrays = new Godot.Collections.Array();
		arrays.Resize((int)ArrayMesh.ArrayType.Max);
		arrays[(int)ArrayMesh.ArrayType.Vertex] = vertices.ToArray();
		arrays[(int)ArrayMesh.ArrayType.Index] = indices.ToArray();

		a_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);
		Mesh = a_mesh;
	}
}
