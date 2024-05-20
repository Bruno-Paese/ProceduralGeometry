@tool
extends StaticBody3D

@export var generate_mesh: bool :
	set(val):
		generate_mesh_func() 

func generate_mesh_func():
	print("generateMesh")
	var plane_mesh = PlaneMesh.new()
	plane_mesh.size = Vector2(4,4)
	plane_mesh.subdivide_depth = 1000
	plane_mesh.subdivide_width = 1000

	$MeshInstance3D.mesh = plane_mesh
	return true
