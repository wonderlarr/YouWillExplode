extends CharacterBody3D

#Control vars
var speed : float = 5.0
var jump_velocity: float = 12

var decel: float = 10
var accel: float = 10

#Exports
@export var camera: Camera3D
@export var raycast: RayCast3D

#Storage vars
var dir: Vector2 = Vector2.ZERO
var jump_buffer: float = 0

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED

func _unhandled_input(event: InputEvent) -> void:
	# If the input is a keyboard input (not sure about gamepad)
	if event is InputEventMouseMotion:
		camera.rotation.x += -event.relative.y * 0.001
		camera.rotation.x = clampf(camera.rotation.x, -PI * 0.5, PI * 0.5)
		rotation.y += -event.relative.x * 0.001
		
	elif event is InputEventKey:
		dir = Input.get_vector("moveleft", "moveright", "forward", "backward")
		if event.is_action_pressed("jump") and not event.is_echo():
			jump_buffer = 0.1
		elif event.is_action_pressed("pause"):
			if Input.mouse_mode == Input.MOUSE_MODE_CAPTURED:
				Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
			else:
				Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
		elif event.is_action_pressed("use"):
			if raycast.is_colliding():
				var collided : Area3D = raycast.get_collider()
				if collided is UsableThing:
					collided.OnUse()
		

func _physics_process(delta: float) -> void:	
	#TODO acceleration based movement
	#Grounded movement
	if dir == Vector2.ZERO:
		#velocity = velocity.move_toward(Vector3.ZERO, decel)
		velocity.x = move_toward(velocity.x, 0, decel)
		velocity.z = move_toward(velocity.z, 0, decel)
	else:
		velocity.x = move_toward(velocity.x, dir.x * speed, accel)
		velocity.z = move_toward(velocity.z, dir.y * speed, accel)
		velocity = velocity.rotated(Vector3.UP, rotation.y)
		
		
	#if is_on_floor():
		#velocity = Vector3(dir.x * speed, velocity.y, dir.y * speed).rotated(Vector3.UP, rotation.y)
	#else:
		#velocity = Vector3(dir.x * speed * 0.65, velocity.y, dir.y * speed * 0.65).rotated(Vector3.UP, rotation.y)
	
	if not is_on_floor():
		velocity += get_gravity() * delta * 3
		
	if jump_buffer > 0:
		jump_buffer -= delta
		if is_on_floor():
			#TODO movement boost when jumping forward
			velocity.y += jump_velocity
		
	move_and_slide()
