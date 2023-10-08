using Godot;
using System;

public partial class MovingIcon : Sprite2D
{
  private int speed = 400;
  private float angularSpeed = Mathf.Pi;

	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("Timer");

		if (timer != null) {
		  timer.Timeout += OnTimerTimeout;
		}
	}

	public override void _Process(double delta)
	{
		Rotation += angularSpeed * (float)delta;
		Vector2 velocity =  Vector2.Up.Rotated(Rotation) * speed;
		Position += velocity * (float)delta;

	}

	private void OnTimerTimeout() {
	  Visible = !Visible;
	}

  private void OnButtonPressed()
  {
	  SetProcess(!IsProcessing());
  }

}
