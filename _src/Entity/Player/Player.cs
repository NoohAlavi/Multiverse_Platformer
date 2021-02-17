using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 200f;
    [Export] public float gravityForce = 200f;

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        
        //Implement Gravity
        if (IsOnFloor()) velocity.y = 0f;
        else velocity.y += gravityForce;

        velocity = velocity.Normalized() * movementSpeed;
        velocity = MoveAndSlide(velocity, Vector2.Up);
    }

    private void GetInput()
    {
        velocity.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        if (Input.IsActionPressed("jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {

    }
}
