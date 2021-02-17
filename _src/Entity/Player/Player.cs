using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity;
    [Export] public float movementSpeed;

    public override void _PhysicsProcess(float delta)
    {
        
    }
}
