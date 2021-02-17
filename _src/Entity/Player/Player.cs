using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 200f;

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
    }

    private void GetInput()
    {
        velocity.x = (Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left")) * movementSpeed;
    }
}
