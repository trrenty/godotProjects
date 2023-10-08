using Godot;
using System;

public partial class Player : Area2D
{

  [Signal]
  public delegate void HitEventHandler();

  [Export]
  public int Speed { get; set; } = 400;

  public Vector2 screenSize;


  public override void _Ready()
  {
    screenSize = GetViewportRect().Size;
    Hide();
  }

  public override void _Process(double delta)
  {
    Vector2 velocity = Vector2.Zero;
    if (Input.IsActionPressed("move_right"))
    {
      velocity.X += 1;
    }

    if (Input.IsActionPressed("move_left"))
    {
      velocity.X -= 1;
    }
    if (Input.IsActionPressed("move_up"))
    {
      velocity.Y -= 1;
    }
    if (Input.IsActionPressed("move_down"))
    {
      velocity.Y += 1;
    }

    AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    if (velocity.Length() > 0)
    {
      Position += velocity.Normalized() * Speed * (float)delta;
      Position = new Vector2(Mathf.Clamp(Position.X, 0, screenSize.X), Mathf.Clamp(Position.Y, 0, screenSize.Y));
      animatedSprite2D.Play();
    }
    else
    {
      animatedSprite2D.Stop();
    }
    if (velocity.X != 0)
    {
      animatedSprite2D.Animation = "walk";
      animatedSprite2D.FlipV = false;
      animatedSprite2D.FlipH = velocity.X < 0;
    }
    else if (velocity.Y != 0)
    {
      animatedSprite2D.Animation = "up";
      animatedSprite2D.FlipV = velocity.Y > 0;
    }
  }

  private void OnBodyEntered(PhysicsBody2D body)
  {
    Hide(); // Player disappears after being hit.
    EmitSignal(SignalName.Hit);
    // Must be deferred as we can't change physics properties on a physics callback.
    GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
  }

  public void Start(Vector2 position)
  {
    Position = position;
    Show();
    GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
  }
}
