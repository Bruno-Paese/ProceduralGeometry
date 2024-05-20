@tool
extends StaticBody3D

@export var generate_mesh: bool :
	set(val):
		generate_mesh_func() 

func generate_mesh_func():
	print("generateMesh")
	var plane_mesh = PlaneMesh.new()
	plane_mesh.size = Vector2(100,100)
	plane_mesh.subdivide_depth = 100
	plane_mesh.subdivide_width = 100

	var surface_tool = SurfaceTool.new()
	surface_tool.create_from(plane_mesh, 0)
	var data = surface_tool.commit_to_arrays()
	var vertices = data[ArrayMesh.ARRAY_VERTEX]
	
	var noise = FastNoiseLite.new()
	noise.noise_type = FastNoiseLite.TYPE_SIMPLEX

	for i in vertices.size():
		vertices[i].y = noise.get_noise_2d(vertices[i].x, vertices[i].z) * 10
	data[ArrayMesh.ARRAY_VERTEX] = vertices

	var array_mesh = ArrayMesh.new()
	array_mesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES, data)

	$MeshInstance3D.mesh = array_mesh
	return true
