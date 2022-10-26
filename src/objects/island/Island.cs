using Godot;

public class Island : Node2D
{
    private Area2D _clickArea;

    [Signal]
    public delegate void OnClick(Island island);

    public override void _Ready()
    {
        _clickArea = GetNode<Area2D>("ClickArea");
        _clickArea.Connect("input_event", this, nameof(OnMouseClick));
    }

    public void OnMouseClick(object viewport, object @event, int shape_idx)
    {
        if (@event is InputEventMouseButton mouse && mouse.IsPressed() && mouse.ButtonIndex == (int)ButtonList.Left)
        {
            EmitSignal(nameof(OnClick), this);
        }
    }
}



