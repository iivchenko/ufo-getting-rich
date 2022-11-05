using Godot;

public class Cloud : Node2D
{
    private Sprite _cloud01;
    private Sprite _cloud02;
    private Sprite _cloud03;

    private Sprite _current;

    [Export]
    public int Speed { get; set; }

    [Export]
    public Vector2 Direction { get; set; }

    [Export]
    public CloudSelection CurrentCloud { get; set; }

    public Vector2 Size => _current.RegionRect.Size * Scale;

    public override void _Ready()
    {
        base._Ready();

        _cloud01 = GetNode<Sprite>("Cloud01");
        _cloud02 = GetNode<Sprite>("Cloud02");
        _cloud03 = GetNode<Sprite>("Cloud03");

        switch (CurrentCloud)
        {
            case CloudSelection.Cloud01:
                _current = _cloud01;
                break;
            case CloudSelection.Cloud02:
                _current = _cloud02;
                break;
            case CloudSelection.Cloud03:
                _current = _cloud03;
                break;
            default:
                _current = _cloud01;
                break;
        }
        _cloud01.Visible = false;
        _current.Visible = true;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        Position += Direction * Speed * delta;
    }

    public enum CloudSelection
    {
        Cloud01, 
        Cloud02,
        Cloud03
    }
}
