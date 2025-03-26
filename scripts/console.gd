extends PanelContainer
#Simple GDScript console script
#TODO limit console to only some built-in commands if in a multiplayer game.

@export var output_label : RichTextLabel
@export var input_edit : LineEdit

var should_restart : bool = false

var command : Expression = Expression.new()
var command_history : Array[String] = []
var command_history_pos : int = 0
var disable_scrolling : bool = false

static var loaded: bool = false

func _ready() -> void:
	# loaded is a static variable, so if any consoles are active, this variable will be set to true already.
	if loaded:
		queue_free()
		print("Possibly two consoles loaded at once! Sacrificing instance.")
		return
	else:
		loaded = true
		print("Console enabled.")
		conprint("Developer Console. Type help() for help.")
	
func _input(event: InputEvent) -> void:
	if event.is_action_pressed("console"):
		accept_event()
		visible = !visible
		if visible:
			input_edit.edit()
			
# Print to console and stdout
func conprint(input : String = "", con_only : bool = false) -> void:
	input = str(input)
	if not con_only:
		#print_rich("Console: " + input)
		pass
	output_label.append_text(input + "\n")

func execute(input: String, bypass_history : bool = false) -> void:
	input = input.strip_edges()
	if input == "":
		return
	
	input_edit.clear()
	if not bypass_history:
		command_history.push_front((str(input)))
		# prevent array from getting too big, we (probably) dont need to fetch history this far back
		if command_history.size() > 100:
			command_history.resize(100)
			
	output_label.push_color(Color.from_string("16A085", Color.TEAL))
	output_label.append_text("] ")
	output_label.pop()
	output_label.append_text(input.replace("[", "[lb]") + "\n") #escaped tags for readability
	
	var error : Error = command.parse(input)
	if error:
		err_parse_bad(str(command.get_error_text()))
		return
	
	var result : Variant = command.execute([], self)
	
	if command.has_execute_failed():
		err_execute_bad(str(command.get_error_text()))
		return
	elif result:
		output_label.push_color(Color.GRAY)
		conprint(str(result))
		output_label.pop()
		
func execute_multi(array: Array[String], bypass_history : bool = false) -> void:
	@warning_ignore("shadowed_variable")
	for command : String in array:
		execute(command, bypass_history)
		
func err_parse_bad(error_text: String = "No error information") -> void:
	output_label.push_color(Color.RED)
	conprint("Parse Error: " + error_text)
	output_label.pop()
	
func err_execute_bad(error_text: String = "No error information") -> void:
	output_label.push_color(Color.RED)
	conprint("Execution Error: " + error_text)
	output_label.pop()
	
func set_should_restart(val: bool) -> void:
	if val:
		conprint("[color=yellow]This setting won't apply until the game is restarted.[/color]")

func _on_line_edit_gui_input(event: InputEvent) -> void:
	if event.is_action_pressed("ui_up", true) and command_history.size() > 0 and (not input_edit.text or not disable_scrolling):
		var history_max : int = command_history.size()
		command_history_pos = clampi(command_history_pos + 1, 0, history_max)
		if command_history_pos == 0:
			input_edit.text = ""
		else:
			input_edit.text = command_history[command_history_pos - 1]
			input_edit.caret_column = input_edit.text.length()
		disable_scrolling = false
		get_viewport().set_input_as_handled()
	if event.is_action_pressed("ui_down", true) and command_history.size() > 0 and (not input_edit.text or not disable_scrolling):
		var history_max : int = command_history.size()
		command_history_pos = clampi(command_history_pos - 1, 0, history_max)
		if command_history_pos == 0:
			input_edit.text = ""
		else:
			input_edit.text = command_history[command_history_pos - 1]
			input_edit.caret_column = input_edit.text.length()
		disable_scrolling = false
		get_viewport().set_input_as_handled()

func _on_line_edit_text_changed(_new_text: String) -> void:
	disable_scrolling = true
	command_history_pos = 0

#===================
# System
#===================
func help() -> void:
	conprint("This console uses GDScript Expressions. As a result, it has access to only a limited number of GDScript features. I haven't finished writing this help command yet, yell at me if you find this.")
	
# Exits the game. TODO currently does not do any special handling or saving.
func quit() -> void:
	conprint("Quitting game. Goodbye!")
	get_tree().quit()
	
# Clears the output buffer
func clear() -> void:
	output_label.clear()
	
#===================
# User Settings
#===================

func r_force_vertex_shading(val: bool) -> void:
	var setting : String = "rendering/shading/overrides/force_vertex_shading"
	ProjectSettings.set_setting(setting, val)
	conprint("Force Vertex Shading: " + str(val))
	if val:
		output_label.push_color(Color.YELLOW)
		conprint("THIS WILL PROBABLY BREAK THINGS")
		output_label.pop()
	set_should_restart(true)
