using Godot;

public sealed class Coin : Area2D
{
    [Signal]
    public delegate void UfoCollided();

    private static PackedScene Packet = GD.Load<PackedScene>("res://objects/coin/coin.tscn");

    public static Coin Create()
    {
        return Packet.Instance<Coin>();
    }

    public override void _Ready()
    {
        base._Ready();

        this.Connect("body_entered", this, nameof(OnUfoCollided));
    }

    private void OnUfoCollided(Node _)
    {        
        EmitSignal(nameof(UfoCollided));

        QueueFree();
    }    
}
