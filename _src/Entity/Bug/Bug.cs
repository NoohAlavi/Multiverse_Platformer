using Godot;

public class Bug : KinematicBody2D
{
    [Export] public Player targetPlayer;
    [Export] public Vector2 velocity = Vector2.Zero;
    [Export] public float movementSpeed = 100f;
    [Export] public float gravity = 200f;
    [Export] public float damage = 1f;

    private Sprite sprite;
    private RayCast2D rayCast;
    
    public override void _Ready()
    {
        targetPlayer = GetNode<Player>("../../../Player");
        GD.Print(targetPlayer);

        sprite = GetNode<Sprite>("Sprite");
        rayCast = GetNode<RayCast2D>("RayCast2D");
        rayCast.Enabled = true;
    }

    public override void _Process(float delta)
    {
        //Make the sprite face the player's direction
        if (velocity.x > 0f)
        {
            sprite.FlipH = true;
        }
        else if (velocity.x < 0f)
        {
            sprite.FlipH = false;
        }

        rayCast.CastTo = (targetPlayer.Position - Position).Normalized() * 50f;
        checkCollisions(targetPlayer);
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

    private void checkCollisions(Player target)
    {
        var collider = rayCast.GetCollider();
        if (collider is Player)
        {
            Player p = (Player)collider;
            if (p == target)
            {
                if (target.health > 0)
                {
                    target.health -= damage;
                }
            }
        } else 
        {
            return;
            
        }
    }
}
