using Godot;
using System;

public partial class Ball : RigidBody2D
{
	public bool reset_state = false;
	public Vector2 moveVector;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		if (reset_state)
		{
			state.Transform = new Transform2D(0, moveVector);
			reset_state = false;
		}
	}
	
	public void Start(Vector2 targetPos)
	{
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		moveVector = targetPos;
		reset_state = true;
	}
	
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
