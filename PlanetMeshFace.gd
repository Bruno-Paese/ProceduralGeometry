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
	noise.seed = randi()

	var axisA = Vector3(Vector3.UP.y, Vector3.UP.z, Vector3.UP.x);
	var axisB = axisA.cross(Vector3.UP);

	for i in vertices.size():
		#vertices[i].y = (1+ noise.get_noise_2d(vertices[i].x, vertices[i].z)) * 10
		vertices[i].y = vertices[i].y + Vector3.UP.y
		vertices[i].x = vertices[i].x * 0.02
		vertices[i].z = vertices[i].z * 0.02
		vertices[i] = vertices[i].normalized() * 100
		 
		
	data[ArrayMesh.ARRAY_VERTEX] = vertices

	var array_mesh = ArrayMesh.new()
	array_mesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES, data)

	$MeshInstance3D.mesh = array_mesh
	return true
