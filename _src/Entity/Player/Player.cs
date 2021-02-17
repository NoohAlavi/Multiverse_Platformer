using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 200f;
    [Export] public float gravity = 20f;
    [Export] public float jumpForce = 400f;

    public override void _PhysicsProcess(float delta)
    {   
        //Implement Gravity
        if (IsOnFloor())
        {
            velocity.y = 0f;
        }
        else velocity.y += gravity;

        GetInput();

        //Calculates Movement
        // velocity = velocity.Normalized() * movementSpeed;
        velocity.x *= movementSpeed;
        velocity = MoveAndSlide(velocity, Vector2.Up);
    }

    private void GetInput()
    {
        velocity.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

        //Call Jump() if player clicks W, Up arrow, spacebar, or A on controller
        if (Input.IsActionPressed("jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!IsOnFloor()) return;
        velocity.y = 0;
        velocity.y -= jumpForce;
    }
}
