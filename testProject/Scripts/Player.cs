using Godot;
using System;

public partial class Player : RigidBody2D
{
    [Export]
    private int Speed {get; set;} = 200;
    
    public override void _Process(double delta)
    {
        Vector2 direction = Vector2.Zero;
        if (Input.IsKeyPressed(Key.Up)) {
             direction.Y -= 1;
        }
        if (Input.IsKeyPressed(Key.Down)) {
            direction.Y += 1;
        }
        if (Input.IsKeyPressed(Key.Right)) {
            direction.X += 1;
        }
        if (Input.IsKeyPressed(Key.Left)) {
          direction.X -= 1;
        }

        Position += direction * Speed * (float)delta;
    }
}
