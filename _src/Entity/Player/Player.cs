using Godot;

public class Player : KinematicBody2D
{
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 350f;
    [Export] public float gravity = 20f;
    [Export] public float jumpForce = 700f;
    [Export] public float maxHealth = 100f;
    [Export] public float health = 100f;

    private AnimatedSprite playerSprite;
    private Label healthLabel;

    public override void _Ready()
    {
        playerSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        health = maxHealth;
        healthLabel  = GetNode<Label>("HUD/CanvasLayer/HealthLabel");
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

        //Check if player fell off the screen

        if (Position.y >= 800f)
        {
            Die();
        }
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

        //Set Healthbar to health val

        healthLabel.Text = health + "%";
    }

    private void GetInput()
    {
        velocity.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

        //Call Jump() if player clicks W, Up arrow, spacebar, or A on controller
        if (Input.IsActionPressed("jump"))
        {
            Jump();
        }

        //Accelerate gravity if player holding "move_down" key

        if (Input.IsActionPressed("move_down"))
        {
            if (!IsOnFloor())
                velocity.y += (gravity * 2);
        }
    }

    private void Jump()
    {
        if (!IsOnFloor()) return; //Do not jump if in air
        velocity.y = 0;
        velocity.y -= jumpForce;
    }

    private void Die()
    {
        GD.Print("Player fell out of world");
        GetTree().ReloadCurrentScene();
    }
}
