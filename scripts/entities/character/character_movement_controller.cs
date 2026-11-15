using Godot;
using System;

[GlobalClass]
public partial class character_movement_controller : Node
{
	[Export]
	public Node2D TargetNode { get; set; } // El nodo que se va a mover (Player, NPC, etc.)

	[Export]
	public float WalkSpeed { get; set; } = 4.0f;

	public const int TileSize = 16;

	private Vector2 _targetPosition;
	private bool _isMoving = false;

	public override void _Ready()
	{
		if (TargetNode == null) return;

		TargetNode.Position = TargetNode.Position.Snapped(Vector2.One * TileSize);
		_targetPosition = TargetNode.Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (TargetNode == null || !_isMoving) return;

		TargetNode.Position = TargetNode.Position.MoveToward(_targetPosition, WalkSpeed * TileSize * (float)delta);

		if (TargetNode.Position.DistanceTo(_targetPosition) < 0.01f)
		{
			TargetNode.Position = _targetPosition;
			_isMoving = false;
		}
	}

	public void OnMoveRequested(Vector2 direction)
	{
		if (TargetNode == null || _isMoving || direction == Vector2.Zero) return;

		_targetPosition = TargetNode.Position + (direction * TileSize);
		_isMoving = true;
	}
}
