using Godot;
using System;

public partial class PauseNode : Node
{	
	
	
	public override void _Input(InputEvent @event)
	{
		GetViewport().SetInputAsHandled();
		if (Input.IsActionPressed("p_pause")) {
			if (GetTree().Paused == true)
			{
				GetTree().Paused = false;
			}
		}
	}
}
