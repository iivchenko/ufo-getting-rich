using Godot;

public class Island : Node2D
{
    private Area2D _clickArea;
    private IslandTarget _target;

    [Signal]
    public delegate void OnClick(Island island);

    public override void _Ready()
    {
        _clickArea = GetNode<Area2D>("ClickArea");
        _target = GetNode<IslandTarget>("Target");

        _clickArea.Connect("input_event", this, nameof(OnMouseClick));
        _clickArea.Connect("mouse_entered", this, nameof(OnMouseEntered));
        _clickArea.Connect("mouse_exited", this, nameof(OnMouseExit));
    }

    public void OnMouseClick(object viewport, object @event, int shape_idx)
    {
        if (@event is InputEventMouseButton mouse && mouse.IsPressed() && mouse.ButtonIndex == (int)ButtonList.Left)
        {
            EmitSignal(nameof(OnClick), this);
        }
    }

    public void OnMouseEntered()
    {
        _target.Visible = true;
        _target.Start();
    }

    public void OnMouseExit()
    {
        _target.Visible = false;
        _target.Stop();
    }
}



