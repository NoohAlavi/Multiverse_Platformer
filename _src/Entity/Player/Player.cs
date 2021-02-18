using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 200f;
    [Export] public float gravity = 20f;
    [Export] public float jumpForce = 400f;

    private Sprite playerSprite;

    public override void _Ready()
    {
        playerSprite = GetNode<Sprite>("Sprite");
    }

    public override void _PhysicsProcess(float delta)
    {   
        //Implement Gravity
        if (IsOnFloor())
        {
            velocity.y = 0f;
        }
        else velocity.y += gravity;

        GetInput();

        //Calculate Movement
        velocity.x *= movementSpeed;
        velocity = MoveAndSlide(velocity, Vector2.Up);
    }

    public override void _Process(float delta)
    {
        //Calculate direction of player, flip sprite to match direction
        if (velocity.x > 0)
        {
            playerSprite.FlipH = false;
        } else if (velocity.x < 0)
        {
            playerSprite.FlipH = true;
        }
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
        if (!IsOnFloor()) return; //Do not jump if in air
        velocity.y = 0;
        velocity.y -= jumpForce;
    }
}
