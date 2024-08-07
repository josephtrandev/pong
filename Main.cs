using Godot;
using System;

public partial class Main : Node
{
	private int _playerScore, _botScore;
	CanvasLayer pause_menu = null;
	
	public override void _Ready()
	{
		pause_menu = GetNode<CanvasLayer>("PauseMenu");
		pause_menu.Visible = false;
	}
	
	public override void _Process(double delta)
	{
		if (pause_menu.Visible)
		{
			pause_menu.Visible = false;
		}
		
	}
	
	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionPressed("p_pause"))
		{
			PauseGame();
		}
	}
	
	public void PauseGame()
	{
		if (!GetNode<Timer>("GameTimer").IsStopped())
		{
			pause_menu.Show();
			GetTree().Paused = true;
		}
	}
	
	public void GameOver()
	{
		_botScore++;
		GetNode<HUD>("HUD").UpdateScore(_playerScore, _botScore);
		
		GetNode<Timer>("BallTimer").Stop();
		GetNode<Timer>("GameTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
	}
	
	public void GameWin()
	{
		_playerScore++;
		GetNode<HUD>("HUD").UpdateScore(_playerScore, _botScore);
		
		GetNode<Timer>("BallTimer").Stop();
		GetNode<Timer>("GameTimer").Stop();
		GetNode<HUD>("HUD").ShowGameWin();
	}
	
	public void NewGame()
	{
		var hud = GetNode<HUD>("HUD");
		hud.ShowMessage("Get Ready!");
		
		// Reset player paddle position
		var player = GetNode<Player>("Player");
		var playerStartPosition = GetNode<Marker2D>("PlayerStartPosition");
		player.Start(playerStartPosition.Position);
		
		// Reset bot paddle position
		var bot = GetNode<Bot>("Bot");
		var botStartPosition = GetNode<Marker2D>("BotStartPosition");
		bot.Start(botStartPosition.Position);
		
		// Stop all velocity on ball and reset position on new game
		var ball = GetNode<Ball>("Ball");
		var velocity = new Vector2(0, 0);
		ball.LinearVelocity = velocity;
		var targetPos = new Vector2(576, 320);
		ball.Start(targetPos);
		
		GetNode<Timer>("StartTimer").Start();
	}
	
	private void OnGameTimerTimeout()
	{
		GetNode<HUD>("HUD").UpdateScore(_playerScore, _botScore);
	}
	
	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("BallTimer").Start();
		GetNode<Timer>("GameTimer").Start();
	}
	
	private void OnBallTimerTimeout()
	{
		// Once game starts, reapply velocity to ball
		var ball = GetNode<Ball>("Ball");
		var velocity = new Vector2(-400, -400);
		ball.LinearVelocity = velocity;
		
	}

}
