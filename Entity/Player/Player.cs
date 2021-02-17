using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity;

    public override void _PhysicsProcess(float delta)
    {
        
    }
}
