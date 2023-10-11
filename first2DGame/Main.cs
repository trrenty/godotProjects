using Godot;
using System;

public partial class Main : Node
{
  [Export]
  public PackedScene MobScene { get; set; }

  private int score;

  public override void _Ready()
  {
    var hud = GetNode<Hud>("Hud");
    hud.UpdateScore(score);
    hud.ShowMessage("Get Ready!");
  }

  public void OnPlayerHit()
  {
    GameOver();
  }

  public void OnScoreTimerTimeout()
  {
    score++;
    GetNode<Hud>("Hud").UpdateScore(score);
  }

  public void OnStartTimerTimeout()
  {
    GetNode<Timer>("MobTimer").Start();
    GetNode<Timer>("ScoreTimer").Start();
  }

  public void GameOver()
  {
    GetNode<Timer>("MobTimer").Stop();
    GetNode<Timer>("ScoreTimer").Stop();

    GetNode<Hud>("Hud").ShowGameOver();
  }

  public void NewGame()
  {
    GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
    score = 0;
    Player player = GetNode<Player>("Player");
    Marker2D startPosition = GetNode<Marker2D>("StartPosition");
    player.Start(startPosition.Position);

    GetNode<Timer>("StartTimer").Start();
  }

  public void OnModTimerTimeout()
  {
    Mob mob = MobScene.Instantiate<Mob>();

    PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
    mobSpawnLocation.ProgressRatio = GD.Randf();

    float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

    mob.Position = mobSpawnLocation.Position;

    // Add some randomness to the direction.
    direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
    mob.Rotation = direction;

    // Choose the velocity.
    var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
    mob.LinearVelocity = velocity.Rotated(direction);

    // Spawn the mob by adding it to the Main scene.
    AddChild(mob);
  }
}
