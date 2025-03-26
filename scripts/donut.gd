extends MeshInstance3D

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	rotation_degrees.y += 0.5
