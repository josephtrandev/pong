using Godot;
using System;

public partial class HUD : CanvasLayer
{
	
	[Signal]
	public delegate void StartGameEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
		
		GetNode<Timer>("MessageTimer").Start();
	}
	
	async public void ShowGameOver()
	{
		ShowMessage("Bot Wins");
		
		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);
		
		ShowMessage("Play again?");
		GetNode<Timer>("MessageTimer").Stop();
		
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("PlayButton").Show();
	}
	
	async public void ShowGameWin()
	{
		ShowMessage("Player Wins");
		
		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);
		
		ShowMessage("Play again?");
		GetNode<Timer>("MessageTimer").Stop();
		
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("PlayButton").Show();
	}
	
	public void UpdateScore(int playerScore, int botScore)
	{
		GetNode<Label>("PlayerScore").Text = playerScore.ToString();
		GetNode<Label>("BotScore").Text = botScore.ToString();
	}
	
	private void OnPlayButtonPressed()
	{
		GetNode<Button>("PlayButton").Hide();
		EmitSignal(SignalName.StartGame);
	}

	private void OnMessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}
	
}
