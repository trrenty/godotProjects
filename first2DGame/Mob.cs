using Godot;
using System;

public partial class Mob : RigidBody2D
{
    public override void _Ready()
    {
      AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
      string[] animationNames = animatedSprite2D.SpriteFrames.GetAnimationNames();
      animatedSprite2D.Play(animationNames[GD.Randi() % animationNames.Length]);
    }

    public void OnScreenExited() {
      QueueFree();
    }
}
