using Godot;
using System;

public partial class Player : StaticBody2D
{
	private float _speed = 500;
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;
		if ((Input.IsActionPressed("ui_up")) | (Input.IsActionPressed("w_up")))
		{
			velocity = Vector2.Up * _speed;
		}
		if ((Input.IsActionPressed("ui_down")) | (Input.IsActionPressed("s_down")))
		{
			velocity = Vector2.Down * _speed;
		}
		
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 100, ScreenSize.Y-100)
		);
	}
	
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}
