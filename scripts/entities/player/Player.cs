using Godot;
using System;

[GlobalClass]
public partial class Player : Node2D
{
	[Signal]
	public delegate void MoveRequestedEventHandler(Vector2 direction);

	[Export]
	public character_movement_controller MovementController { get; set; }

	public override void _Ready()
	{
		// Conectamos la señal del jugador al método del controlador
		if (MovementController != null)
		{
			MoveRequested += MovementController.OnMoveRequested;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = GetInputDirection();
		
		if (direction != Vector2.Zero)
		{
			EmitSignal(SignalName.MoveRequested, direction);
		}
	}

	private Vector2 GetInputDirection()
	{
		if (Input.IsActionPressed("buttonLeft")) return Vector2.Left;
		if (Input.IsActionPressed("buttonRight")) return Vector2.Right;
		if (Input.IsActionPressed("buttonUp")) return Vector2.Up;
		if (Input.IsActionPressed("buttonDown")) return Vector2.Down;

		return Vector2.Zero;
	}
}
