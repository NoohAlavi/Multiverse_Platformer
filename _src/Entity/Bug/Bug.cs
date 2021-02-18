using Godot;
using System;

public class Bug : KinematicBody2D
{
    [Export] public Player targetPlayer;
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 100f;
    [Export] public float gravity = 200f;
    
    public override void _Ready()
    {
        targetPlayer = GetNode<Player>("../../../Player");
        GD.Print(targetPlayer);
    }

    public override void _PhysicsProcess(float delta)
    {
        //Calculate Direction
        followPlayer(targetPlayer);
        velocity *= movementSpeed;

        //Implement Gravity
        if (IsOnFloor())
        {
            velocity.y = 0f;
        }
        else velocity.y += gravity;

        velocity = MoveAndSlide(velocity, Vector2.Up);
    }

    private void followPlayer(Player target)
    {
        Vector2 targetPos = target.Position;
        velocity = new Vector2(targetPos.x - Position.x, Position.y).Normalized();
    }
}
