using Godot;
using System;

public partial class BotGoal : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node2D body)
	{
		EmitSignal(SignalName.Hit);
	}
}
