using Godot;

public class IslandBase : Node2D
{
    private Area2D _clickArea;

    [Signal]
    public delegate void MouseClicked(IslandBase island);

    [Signal]
    public delegate void MouseEntered(IslandBase island);

    [Signal]
    public delegate void MouseExited(IslandBase island);

    public override void _Ready()
    {
        _clickArea = GetNode<Area2D>("ClickArea");

        _clickArea.Connect("input_event", this, nameof(OnMouseClick));
        _clickArea.Connect("mouse_entered", this, nameof(OnMouseEntered));
        _clickArea.Connect("mouse_exited", this, nameof(OnMouseExit));
    }

    private void OnMouseClick(object viewport, object @event, int shape_idx)
    {
        if (@event is InputEventMouseButton mouse && mouse.IsPressed() && mouse.ButtonIndex == (int)ButtonList.Left)
        {
            EmitSignal(nameof(MouseClicked), this);
        }
    }

    private void OnMouseEntered()
    {
        EmitSignal(nameof(MouseEntered), this);
    }

    private void OnMouseExit()
    {
        EmitSignal(nameof(MouseExited), this);
    }
}
