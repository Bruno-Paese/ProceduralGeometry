@tool
extends Node3D

@export var generate_mesh: bool :
	set(val):
		_ready() 

func _ready():
	for child in get_children():
		if child.has_method("generate_mesh_func"):
			child.generate_mesh_func()
