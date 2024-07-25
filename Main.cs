using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene BallScene { get; set; }
	
	private int _gameTime;
	
	public override void _Ready()
	{
	}
	
	public void GameOver()
	{
		GetNode<Timer>("BallTimer").Stop();
		GetNode<Timer>("GameTimer").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
	}
	
	public void GameWin()
	{
		GetNode<Timer>("BallTimer").Stop();
		GetNode<Timer>("GameTimer").Stop();
		GetNode<HUD>("HUD").ShowGameWin();
	}
	
	public void NewGame()
	{
		_gameTime = 0;
		
		var hud = GetNode<HUD>("HUD");
		hud.UpdateTime(_gameTime);
		hud.ShowMessage("Get Ready!");
		
		var player = GetNode<Player>("Player");
		var playerStartPosition = GetNode<Marker2D>("PlayerStartPosition");
		player.Start(playerStartPosition.Position);
		
		var bot = GetNode<Bot>("Bot");
		var botStartPosition = GetNode<Marker2D>("BotStartPosition");
		bot.Start(botStartPosition.Position);
		
		var ball = GetNode<Ball>("Ball");
		var ballStartPosition = GetNode<Marker2D>("BallStartPosition");
		ball.Start(ballStartPosition.Position);
		
		GetNode<Timer>("StartTimer").Start();
	}
	
	private void OnGameTimerTimeout()
	{
		_gameTime++;
		GetNode<HUD>("HUD").UpdateTime(_gameTime);
	}
	
	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("BallTimer").Start();
		GetNode<Timer>("GameTimer").Start();
	}
	
	private void OnBallTimerTimeout()
	{
		var ball = GetNode<Ball>("Ball");
		var velocity = new Vector2(x: -400, y: -400);
		ball.LinearVelocity = velocity;
	}

}
