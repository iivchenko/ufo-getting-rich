using Godot;

public sealed class Coin : Area2D
{
    [Signal]
    public delegate void UfoCollided();

    private static PackedScene Packet = GD.Load<PackedScene>("res://objects/coin/coin.tscn");

    private AudioStreamPlayer _sfx;

    public static Coin Create()
    {
        return Packet.Instance<Coin>();
    }

    public override void _Ready()
    {
        base._Ready();

        _sfx = GetNode<AudioStreamPlayer>("Sfx");
        _sfx.Connect("finished", this, nameof(Remove));


        this.Connect("body_entered", this, nameof(OnUfoCollided));
    }

    private void OnUfoCollided(Node _)
    {
        _sfx.Play();
        EmitSignal(nameof(UfoCollided));
        Visible = false;
    }

    private void Remove()
    {
        QueueFree();
    }
}
