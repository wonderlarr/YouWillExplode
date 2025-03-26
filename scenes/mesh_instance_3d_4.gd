extends MeshInstance3D

@export var speed: float = 0.1

func _process(delta: float) -> void:
	rotation.y = fposmod(rotation.y + speed * delta, PI * 2)
